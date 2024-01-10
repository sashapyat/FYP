using Core.Persistence.Context;
using Core.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Standard.Licensing;
using Standard.Licensing.Security.Cryptography;
using Stripe;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Security;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Timers;
using VirtualLibrary.Models;
using VirtualLibrary.Service;

namespace VirtualLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserDataService _userDataService;
        private readonly AppDbContext _context;
        private readonly IBooksService _booksService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, IUserDataService userDataService, AppDbContext context, IBooksService booksService, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _userDataService = userDataService;
            _context = context;
            _booksService = booksService;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> IndexAsync(string Msg)
        {
            ViewBag.Message = Msg;
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.Where(x => x.LoginUserId.Equals(currentUserId)).FirstOrDefault();

            if (user == null)
            {
                return View();
            }
            else
            {
                var model = new BooksModel
                {
                    WantToRead = _booksService.GetBooksInWantToRead(user.UserId),
                    CurrentlyReading = _booksService.GetBooksInCurrentlyReading(user.UserId),
                    AlreadyRead = _booksService.GetBooksInAlreadyRead(user.UserId)
                };

                return View(model);
            }
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}