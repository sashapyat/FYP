using Core.Persistence.Context;
using Core.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using Standard.Licensing;
using Stripe;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Diagnostics.Metrics;
using System.Security.Claims;
using VirtualLibrary.Models;

namespace VirtualLibrary.Service
{
    public class HostedService : IHostedService, IDisposable
    {
        private Timer _timerCharge;
        private Timer _timerCancel;
        private readonly IServiceScopeFactory _scopeFactory;
        public HostedService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timerCharge = new Timer(ChargeMembership, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            _timerCancel = new Timer(CancelMembership, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timerCharge?.Change(Timeout.Infinite, 0);
            _timerCancel?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }


        public async void ChargeMembership(object state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var _emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                DateTime currentDateTime = DateTime.Now;
                DateTime currentDateTimeMinusOneMonth = currentDateTime.AddMonths(-1);

                List<MemberModel> RenewalDue = (from m in _context.Membership
                                            join p in _context.Payment on m.MembershipId equals p.MembershipId
                                            where m.EndDate == null &&
                                            m.IsActive == true &&
                                            (from p2 in _context.Payment
                                              join m2 in _context.Membership on p2.MembershipId equals m2.MembershipId
                                              where m2.MembershipId == p2.MembershipId &&
                                              m2.MembershipId == m.MembershipId &&
                                              m2.IsActive == true
                                              group p2 by p2.MembershipId into p3
                                              select p3.Max(x => x.PaymentDate)).FirstOrDefault() <= currentDateTimeMinusOneMonth
                                            select new MemberModel
                                            {
                                                MembershipId = m.MembershipId,
                                                StartDate = m.StartDate,
                                                EndDate = m.EndDate,
                                                TotalBooks = m.TotalBooks,
                                                UserId = m.UserId,
                                                Customer = m.Customer,
                                                IsActive = m.IsActive
                                            }).Distinct().ToList();


                if (RenewalDue == null || RenewalDue.Count == 0) return;

                foreach(var r in RenewalDue)
                {
                    var user = _context.Users.Where(x => x.UserId == r.UserId).FirstOrDefault();
                    var membership = _context.Membership.Where(x => x.UserId == r.UserId && x.IsActive == true).FirstOrDefault();
                    var chargeService = new ChargeService();
                    var customerService = new CustomerService();

                    var customer = customerService.Get(membership.Customer);
                    var charge = chargeService.Create(new ChargeCreateOptions
                    {
                        Amount = 500,
                        Description = "Membership Renewal",
                        Currency = "eur",
                        ReceiptEmail = user.Email,
                        Customer = customer.Id
                    });

                    Payment p = new Payment
                    {
                        MembershipId = r.MembershipId,
                        PaymentDate = currentDateTime
                    };

                    _context.Payment.Add(p);
                    _context.SaveChanges();


                    membership.TotalBooks = 0;
                    await _context.SaveChangesAsync();


                    UserEmailOptions options = new UserEmailOptions
                    {
                        ToEmails = new List<string>() { user.Email },
                        PlaceHolders = new List<KeyValuePair<string, string>>()
                                {
                                    new KeyValuePair<string, string>("{{FirstName}}", user.FirstName),
                                    new KeyValuePair<string, string>("{{LastName}}", user.LastName)
                                }
                    };

                    await _emailService.SendOngoingPaymentConfirmationEmail(options);

                }

                
            }

        }

        public async void CancelMembership(object state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var _emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                DateTime currentDateTime = DateTime.Now;

                List<MemberModel> CancellationDue = (from m in _context.Membership
                                            where m.EndDate <= currentDateTime &&
                                            m.IsActive == true
                                            select new MemberModel
                                            {
                                                MembershipId = m.MembershipId,
                                                StartDate = m.StartDate,
                                                EndDate = m.EndDate,
                                                TotalBooks = m.TotalBooks,
                                                UserId = m.UserId,
                                                Customer = m.Customer,
                                                IsActive = m.IsActive
                                            }).ToList();


                if (CancellationDue == null || CancellationDue.Count == 0) return;

                foreach (var c in CancellationDue)
                {
                    var userManager = (UserManager<IdentityUser>)scope.ServiceProvider.GetService(typeof(UserManager<IdentityUser>));
                    var roleManager = (RoleManager<IdentityRole>)scope.ServiceProvider.GetService(typeof(RoleManager<IdentityRole>));
                    var singInManager = (SignInManager<IdentityUser>)scope.ServiceProvider.GetService(typeof(SignInManager<IdentityUser>));
                    var membership = _context.Membership.Where(x => x.UserId == c.UserId && x.IsActive == true).FirstOrDefault();

                    Users u = (from u1 in _context.Users
                                         where u1.UserId == c.UserId
                                         select new Users
                                         {
                                             UserId = u1.UserId,
                                             FirstName = u1.FirstName,
                                             LastName = u1.LastName,
                                             Email = u1.Email,
                                             Username = u1.Username,
                                             LoginUserId = u1.LoginUserId
                                         }).FirstOrDefault();

                    var role = await roleManager.FindByIdAsync(u.LoginUserId);
                    var user = await userManager.FindByIdAsync(u.LoginUserId);
                    var currentRoles = await userManager.GetRolesAsync(user);

                    if (currentRoles.Contains("RegisteredUser")) { return; }

                    DigitalLicence dl = (from d in _context.DigitalLicence
                                         where d.UserId == c.UserId
                                         select new DigitalLicence
                                         {
                                             LicenceId = d.LicenceId,
                                             UserId = d.UserId,
                                             LicenceKey = d.LicenceKey,
                                             LicenceXml = d.LicenceXml,
                                             PublicKey = d.PublicKey
                                         }).FirstOrDefault(); 
                    

                    await userManager.RemoveFromRolesAsync(user, currentRoles);
                    await userManager.AddToRoleAsync(user, "RegisteredUser");
                    await singInManager.SignOutAsync();

                    _context.DigitalLicence.Remove(dl);

                    membership.IsActive = false;
                    await _context.SaveChangesAsync();

                    UserEmailOptions options = new UserEmailOptions
                    {
                        ToEmails = new List<string>() { u.Email },
                        PlaceHolders = new List<KeyValuePair<string, string>>()
                                {
                                    new KeyValuePair<string, string>("{{FirstName}}", u.FirstName),
                                    new KeyValuePair<string, string>("{{LastName}}", u.LastName)
                                }
                    };

                    await _emailService.SendMembershipHasBeenCancelledEmail(options);

                }

            }
        }


        public void Dispose()
        {
            _timerCharge.Dispose();
            _timerCancel.Dispose();
        }
    }
}
