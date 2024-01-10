using Core.Persistence.Context;
using Core.Persistence.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Standard.Licensing;
using Standard.Licensing.Security.Cryptography;
using Stripe;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Security;
using System;
using System.ComponentModel;
using System.Net;
using System.Security.Claims;
using VirtualLibrary.Models;
using VirtualLibrary.Service;

namespace VirtualLibrary.Controllers
{
    public class MemberController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUserDataService _userDataService;

        public MemberController(IConfiguration configuration,
            IEmailService emailService,
            AppDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IUserDataService userDataService)
        {
            _configuration = configuration;
            _emailService = emailService;
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _userDataService = userDataService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MembershipDetails(string Msg)
        {
            ViewBag.Message = Msg;
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var u = _context.Users.Where(x => x.LoginUserId.Equals(currentUserId)).FirstOrDefault();
            ViewBag.UserEmail = u.Email;

            var member = _userDataService.GetMemberById(u.UserId);
            if(member == null)
            {
                return View();
            }
            else
            {
                if(member.EndDate != null)
                {
                    string msg = "Your membership will end on " + member.EndDate.Value.Day + "/" + member.EndDate.Value.Month + "/" + member.EndDate.Value.Year;
                    ViewBag.EndDate = msg;
                }
                return View(member);
            }
        }
        [HttpGet]
        public IActionResult Charge()
        {

            return View();
        }

        public async Task<IActionResult> ChargeAsync(string stripeEmail, string stripeToken)
        {
            try
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var u = _context.Users.Where(x => x.LoginUserId.Equals(currentUserId)).FirstOrDefault();

                //
                // Stripe charge code: https://www.youtube.com/watch?v=Iisp6g88IU4
                //

                var customerService = new CustomerService();
                var chargeService = new ChargeService();

                var customer = customerService.Create(new CustomerCreateOptions
                {
                    Email = stripeEmail,
                    Source = stripeToken,
                });

                var charge = chargeService.Create(new ChargeCreateOptions
                {
                    Amount = 500,
                    Description = "Become a Member",
                    Currency = "eur",
                    ReceiptEmail = u.Email,
                    Customer = customer.Id
                });


                var role = await _roleManager.FindByIdAsync(u.LoginUserId);
                var user = await _userManager.FindByIdAsync(u.LoginUserId);
                var currentRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                await _userManager.AddToRoleAsync(user, "PaidMember");

                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(user, false, null);

                Membership m = new Membership
                {
                    UserId = u.UserId,
                    TotalBooks = 0,
                    StartDate= DateTime.Now,
                    EndDate = null,
                    Customer = customer.Id,
                    IsActive= true
                };

                _context.Membership.Add(m);
                _context.SaveChanges();

                Payment p = new Payment
                {
                    MembershipId = m.MembershipId,
                    PaymentDate = DateTime.Now
                };

                _context.Payment.Add(p);
                _context.SaveChanges();

                UserEmailOptions options = new UserEmailOptions
                {
                    ToEmails = new List<string>() { u.Email },
                    PlaceHolders = new List<KeyValuePair<string, string>>()
                                {
                                    new KeyValuePair<string, string>("{{FirstName}}", u.FirstName),
                                    new KeyValuePair<string, string>("{{LastName}}", u.LastName)
                                }
                };

                await _emailService.SendPaymentConfirmationEmail(options);

                KeyGenerator keyGenerator = KeyGenerator.Create();
                KeyPair keyPair = keyGenerator.GenerateKeyPair();
                string passPhrase = "lf81/Oi1Y7mNx2B1bOPkMisFbqVx/aioURAbNm+gEpU=";
                string privateKey = keyPair.ToEncryptedPrivateKeyString(passPhrase);
                string publicKey = keyPair.ToPublicKeyString();

                Console.WriteLine("Private key: {0}", privateKey);
                Console.WriteLine("Public key : {0}", publicKey);


                string deviceIdentifier = Environment.MachineName;
                string customerName = u.FirstName + " " + u.LastName;

                Standard.Licensing.License newLicense = Standard.Licensing.License.New()
                    .WithUniqueIdentifier(Guid.NewGuid())
                    .WithAdditionalAttributes(new Dictionary<string, string>
                    {
                    { "DeviceIdentifier", deviceIdentifier }
                    })
                    .LicensedTo((c) => c.Name = customerName)
                    .CreateAndSignWithPrivateKey(privateKey, passPhrase);

                string licenseXml = newLicense.ToString();
                string licenseKey = Base64UrlEncoder.Encode(newLicense.ToString());

                DigitalLicence l = new DigitalLicence
                {
                    UserId = u.UserId,
                    LicenceKey = licenseKey,
                    LicenceXml = licenseXml,
                    PublicKey = publicKey
                };

                _context.DigitalLicence.Add(l);
                _context.SaveChanges();

                return View();
            }
            catch
            {
                string msg = "Error - Card has been declined";
                return RedirectToAction("MembershipDetails", new { Msg = msg });
            }
            
        }

        [HttpGet]
        public IActionResult EndMembership()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var u = _context.Users.Where(x => x.LoginUserId.Equals(currentUserId)).FirstOrDefault();
            var member = _userDataService.GetMemberById(u.UserId);

            if(member.EndDate != null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View();
        }

        public async Task<IActionResult> ActionEndMembershipAsync()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.Where(x => x.LoginUserId.Equals(currentUserId)).FirstOrDefault();

            var membership = _context.Membership.Where(x => x.UserId == user.UserId && x.IsActive == true).FirstOrDefault();

            DateTime dateTime = new DateTime();

            if (DateTime.Now.Day < membership.StartDate.Day)
            {
                dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, membership.StartDate.Day);
            }
            else
            {
                dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(1).Month, membership.StartDate.Day);
            }

            membership.EndDate = dateTime;
            _context.SaveChangesAsync();

            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                                {
                                    new KeyValuePair<string, string>("{{FirstName}}", user.FirstName),
                                    new KeyValuePair<string, string>("{{LastName}}", user.LastName),
                                    new KeyValuePair<string, string>("{{Date}}", dateTime.Day + "/" + dateTime.Month + "/" + dateTime.Year)
                                }
            };

            await _emailService.SendCancelMembershipEmail(options);



            string msg = "Your membership will end on " + dateTime.Day + "/" + dateTime.Month + "/" + dateTime.Year;
            return RedirectToAction("MembershipDetails", new { Msg = msg });
        }

    }
}
