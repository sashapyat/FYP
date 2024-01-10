using Core.Persistence.Context;
using Core.Persistence.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using VirtualLibrary.Models;
using VirtualLibrary.Service;

namespace VirtualLibrary.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;

        private readonly DbContextOptions<AppDbContext> _contextOptions;
        private readonly IConfiguration _configuration;
        private string _connectionString;
        DbContextOptionsBuilder<AppDbContext> _optionsBuilder;
        public AccountController(ILogger<AccountController> logger,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration,
            AppDbContext context,
            IEmailService emailService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _configuration = configuration;
            _optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            _connectionString = _configuration.GetConnectionString("AppDbConnection");
            _optionsBuilder.UseSqlServer(_connectionString);
            _emailService = emailService;
        }

        public IActionResult Index(string Msg)
        {
            ViewBag.Message = Msg;
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string Msg)
        {
            ViewBag.Message = Msg;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserModel userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var emailExists = _context.Users.Any(u => u.Email == userModel.Email);

                    if (!emailExists)
                    {
                        var usernameExists = _context.Users.Any(u => u.Username == userModel.Username);
                        if (usernameExists)
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Username Already Exists" });
                        }
                        var user = new IdentityUser
                        {
                            UserName = userModel.Username,
                            Email = userModel.Email
                        };
                        var result = await _userManager.CreateAsync(user, userModel.Password);
                        var result2 = await _userManager.AddToRoleAsync(user, "RegisteredUser");
                        if (result.Succeeded && result2.Succeeded)
                        {
                            Users u = new Users
                            {
                                FirstName = userModel.FirstName,
                                LastName = userModel.LastName,
                                Email = userModel.Email,
                                Username = userModel.Username,
                                LoginUserId = user.Id
                            };
                            

                            _context.Users.Add(u);
                            _context.SaveChanges();

                            BookLists ubl1 = new BookLists { BookListName = "Want to read", UserId = u.UserId };
                            BookLists ubl2 = new BookLists { BookListName = "Currently reading", UserId = u.UserId };
                            BookLists ubl3 = new BookLists { BookListName = "Already read", UserId = u.UserId };

                            _context.BookLists.Add(ubl1);
                            _context.BookLists.Add(ubl2);
                            _context.BookLists.Add(ubl3);
                            _context.SaveChanges();

                            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                            if (!string.IsNullOrEmpty(token))
                            {
                                await SendConfirmationEmail(u, token);
                            }

                            return Json(new { redirectToUrl = Url.Action("Login", "Account"), isRedirect = true });
                        }
                        else
                        {
                            //return 1st error
                            foreach (var error in result.Errors)
                            {
                                _logger.LogWarning("Code: {0} {1}", error.Code, error.Description);
                                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"User Registration Failed: {error.Code} {error.Description}" });
                            }
                        }
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Email Already Exists" });
                    }
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Invalid Data - Registration Failed" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error posting register user");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }

        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string uid, string token)
        {
            string msg = "";
            if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(token))
            {
                token = token.Replace(' ', '+');
                var result = await ConfirmEmailAsync(uid, token);
                if (result.Succeeded)
                {
                    using (_context)
                    {
                        var user = _context.Users.Where(u => u.LoginUserId == uid);
                    }
                    msg = "Email successfully verified! You may now log in.";
                    return RedirectToAction("Login", new { Msg = msg });
                }
            }
            return View();
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string uid, string token)
        {
            return await _userManager.ConfirmEmailAsync(await _userManager.FindByIdAsync(uid), token);
        }

        private async Task SendConfirmationEmail(Users u, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configuration.GetSection("Application:EmailConfirmation").Value;

            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { u.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                                {
                                    new KeyValuePair<string, string>("{{FirstName}}", u.FirstName),
                                    new KeyValuePair<string, string>("{{LastName}}", u.LastName),
                                    new KeyValuePair<string, string>("{{Username}}", u.Username),
                                    new KeyValuePair<string, string>("{{Link}}", string.Format(appDomain + confirmationLink, u.LoginUserId, token))
                                }
            };

            await _emailService.SendConfirmationEmailForRegistration(options);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel users)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(users.Username, users.Password, false, false) ;

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string msg = "Login Failed - Incorrect Username or Password";
                    return RedirectToAction("Login", new { Msg = msg });
                }
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous, HttpGet()]
        public IActionResult ForgotPassword(string Msg)
        {
            ViewBag.Message = Msg;
            return View();
        }


        [AllowAnonymous, HttpPost()]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var emailExists = _context.Users.Any(u => u.Email == model.Email);
                if (emailExists)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user != null)
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                        if (!string.IsNullOrEmpty(token))
                        {
                            await SendForgotPasswordEmail(user, token);
                        }
                        else
                        {
                            _logger.LogError("GeneratePasswordResetTokenAsync Error.  No token returned");
                        }
                    }
                    else
                    {
                        _logger.LogError("ForgotPassword Error.  FindByEmailAsync failed for {0}", model.Email);
                    }

                    ModelState.Clear();
                    model.EmailSent = true;
                }
                else
                {
                    string msg = "Email Doesn't Exist In The System. Please Try Again.";

                    return RedirectToAction("ForgotPassword", new { Msg = msg });
                }


            }
            return View(model);
        }

        private async Task SendForgotPasswordEmail(IdentityUser u, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configuration.GetSection("Application:ForgotPassword").Value;
            var user = _context.Users.Where(x => x.LoginUserId.Equals(u.Id)).FirstOrDefault();

            if (user != null)
            {
                var options = new UserEmailOptions
                {
                    ToEmails = new List<string>() { u.Email },
                    PlaceHolders = new List<KeyValuePair<string, string>>()
                                {
                                    new KeyValuePair<string, string>("{{FirstName}}", user.FirstName),
                                    new KeyValuePair<string, string>("{{LastName}}", user.LastName),
                                    new KeyValuePair<string, string>("{{Link}}", string.Format(appDomain + confirmationLink, u.Id, token))
                                }
                };

                await _emailService.SendForgotPasswordEmail(options);
            }
            else
            {
                _logger.LogError("SendForgotPasswordEmail Error.  User for LoginUserId {0} not found", u.Id);
            }

        }

        [AllowAnonymous, HttpGet("reset-password")]
        public IActionResult ResetPassword(string uid, string token)
        {
            var resetPasswordModel = new ResetPasswordModel
            {
                Token = token,
                UserId = uid
            };
            return View(resetPasswordModel);
        }

        [AllowAnonymous, HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                model.Token = model.Token.Replace(' ', '+');
                var result = await ResetPasswordAsync(model);
                if (result.Succeeded)
                {
                    ModelState.Clear();
                    model.IsSuccess = true;
                    return View(model);
                }

                foreach (var error in result.Errors)
                {
                    _logger.LogError("ResetPassword Pswd Error for userId {0}.  Error:{1}", model.UserId, error.Description);
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(model);
        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model)
        {
            _logger.LogDebug("ResetPassword for userId {0}, token:{1} to pswd:{2}", model.UserId, model.Token, model.NewPassword);
            return await _userManager.ResetPasswordAsync(await _userManager.FindByIdAsync(model.UserId), model.Token, model.NewPassword);
        }


    }
}
