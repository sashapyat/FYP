using Core.Persistence.Context;
using Core.Persistence.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NuGet.Frameworks;
using Standard.Licensing;
using Standard.Licensing.Validation;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Security;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using VirtualLibrary.Models;
using VirtualLibrary.Service;

namespace VirtualLibrary.Controllers
{
    public class BookController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly IBooksService _booksService;
        private readonly IDigitalLiscenceService _digitalLiscenceService;

        private readonly DbContextOptions<AppDbContext> _contextOptions;
        private readonly IConfiguration _configuration;
        private string _connectionString;
        DbContextOptionsBuilder<AppDbContext> _optionsBuilder;
        public BookController(ILogger<AccountController> logger,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration,
            AppDbContext context,
            IBooksService booksService,
            IDigitalLiscenceService digitalLiscenceService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _configuration = configuration;
            _optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            _connectionString = _configuration.GetConnectionString("AppDbConnection");
            _optionsBuilder.UseSqlServer(_connectionString);
            _booksService = booksService;
            _digitalLiscenceService = digitalLiscenceService;
        }

        public IActionResult Index(string Msg)
        {
            ViewBag.Message = Msg;
            return View();
        }

        [HttpGet]
        public IActionResult ExploreBooks(string Msg)
        {
            ViewBag.Message = Msg;
            var model = new BooksAndGenresModel
            {
                Books = _booksService.GetAllBooks(),
                GenresList = _booksService.GetAllGenresSelectList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult ExploreBooks(string genreId, string forNoErrors)
        {
            var model = new BooksAndGenresModel
            {
                Books = _booksService.GetAllBooks(),
                GenresList = _booksService.GetAllGenresSelectList()
            };
            SelectList genresList = model.GenresList;
            SelectListItem selectedGenre = genresList.ToList().Find(p => p.Value == genreId);


            var newModel = new BooksAndGenresModel
            {
                Books = _booksService.GetBooksByGenre(Convert.ToInt32(selectedGenre.Value)),
                GenresList = _booksService.GetAllGenresSelectList()
            };

            if(newModel.Books != null)
            {
                return View(newModel);
            }
            else
            {
                string msg = "There are currently no books with the '" + selectedGenre.Text + "' genre.";
                return RedirectToAction("ExploreBooks", new { Msg = msg });
            }
        }

        [HttpGet]
        public IActionResult BookDetails(int id, string Msg)
        {
            ViewBag.Message = Msg;
            var model = _booksService.GetBookById(id);

            if(model == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult AddToList(BooksModel model)
        {
            int bookId = model.BookId;
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.Where(x => x.LoginUserId.Equals(currentUserId)).FirstOrDefault();

            var test1 = _context.BookLists.Where(x => x.UserId.Equals(user.UserId)).Where(y => y.BookListName.Equals("Want to read")).FirstOrDefault();
            var test2 = _context.BookLists.Where(x => x.UserId.Equals(user.UserId)).Where(y => y.BookListName.Equals("Already read")).FirstOrDefault();
            var test3 = _context.UserBookLists.Where(b => b.BookId == bookId).Where(b => b.BookListId == test1.BookListId).FirstOrDefault();
            var test4 = _context.UserBookLists.Where(b => b.BookId == bookId).Where(b => b.BookListId == test2.BookListId).FirstOrDefault();


            if (test1 != null && test3 != null)
            {
                string msg = "You already have this book saved in this list";
                return RedirectToAction("BookDetails", new { id = bookId, Msg = msg});
            }
            else if (test2 != null && test4 != null)
            {
                string msg = "You already have this book saved in \"Already read\"";
                return RedirectToAction("BookDetails", new { id = bookId, Msg = msg });
            }
            else
            {
                var bookListId = _context.BookLists.Where(x => x.UserId.Equals(user.UserId)).Where(y => y.BookListName.Equals("Want to read")).FirstOrDefault();
                UserBookLists ubl = new UserBookLists { BookListId = bookListId.BookListId, BookId = bookId };

                _context.UserBookLists.Add(ubl);
                _context.SaveChanges();

                string msg = "Book has been saved in list";
                return RedirectToAction("BookDetails", new { id = bookId, Msg = msg });
            }

        }

        [HttpPost]
        public async Task<ActionResult> ReadBookAsync(BooksModel model)
        {
            int bookId = model.BookId;
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.Where(x => x.LoginUserId.Equals(currentUserId)).FirstOrDefault();

            var bookListTest = _context.BookLists.Any(u => u.UserId == user.UserId);
            var bookList = _booksService.GetBooksInCurrentlyReading(user.UserId);

            if (bookListTest.Equals(true) && bookList.Any(b => b.BookId == bookId).Equals(true))
            {
                var digitalLicence = _digitalLiscenceService.GetDigitalLicenceByUserId(user.UserId);
                string currentDeviceIdentifier = Environment.MachineName;
                License license = License.Load(Base64UrlEncoder.Decode(digitalLicence.LicenceKey));

                var validationFailures =
                license.Validate()
                       .ExpirationDate()
                       .And()
                       .Signature(digitalLicence.PublicKey)
                       .And()
                       .AssertThat(lic => // Check Device Identifier matches.
                           lic.AdditionalAttributes.Get("DeviceIdentifier") == currentDeviceIdentifier,
                           new GeneralValidationFailure()
                           {
                               Message = "Invalid Device.",
                               HowToResolve = "Contact the supplier to obtain a new license key."
                           })
                       .AssertValidLicense();

                if (validationFailures.Any())
                {
                    throw new UnauthorizedAccessException(validationFailures.First().Message);
                }
                else
                {
                    TempData["openModal"] = "Open Modal";
                    return RedirectToAction("BookDetails", new { id = bookId });
                }

            }
            else
            {
                var digitalLicence = _digitalLiscenceService.GetDigitalLicenceByUserId(user.UserId);
                string currentDeviceIdentifier = Environment.MachineName;
                License license = License.Load(Base64UrlEncoder.Decode(digitalLicence.LicenceKey));

                var validationFailures =
                license.Validate()
                       .ExpirationDate()
                       .And()
                       .Signature(digitalLicence.PublicKey)
                       .And()
                       .AssertThat(lic => // Check Device Identifier matches.
                           lic.AdditionalAttributes.Get("DeviceIdentifier") == currentDeviceIdentifier,
                           new GeneralValidationFailure()
                           {
                               Message = "Invalid Device.",
                               HowToResolve = "Contact the supplier to obtain a new license key."
                           })
                       .AssertValidLicense();

                if (validationFailures.Any())
                {
                    throw new UnauthorizedAccessException(validationFailures.First().Message);
                }
                else
                {
                    var member = _context.Membership.Where(x => x.UserId== user.UserId && x.IsActive == true).FirstOrDefault();

                    if(member.TotalBooks == 3)
                    {
                        string msg = "You have reached the monthly limit";
                        return RedirectToAction("BookDetails", new { id = bookId, Msg = msg });
                    }
                    else
                    {
                        Membership membership = new Membership
                        {
                            MembershipId = member.MembershipId,
                            StartDate = member.StartDate,
                            EndDate = member.EndDate,
                            TotalBooks = member.TotalBooks + 1,
                            UserId = member.UserId,
                            Customer = member.Customer,
                            IsActive = member.IsActive                            
                        };

                        _context.Membership.Remove(member);
                        _context.Membership.Add(membership);
                        await _context.SaveChangesAsync();

                        var bookListId = _context.BookLists.Where(x => x.UserId.Equals(user.UserId)).Where(y => y.BookListName.Equals("Currently reading")).FirstOrDefault();
                        UserBookLists ubl = new UserBookLists { BookListId = bookListId.BookListId, BookId = bookId };

                        _context.UserBookLists.Add(ubl);
                        _context.SaveChanges();

                        var wantToReadBookListId = _context.BookLists.Where(x => x.UserId.Equals(user.UserId)).Where(y => y.BookListName.Equals("Want to read")).FirstOrDefault();

                        if (wantToReadBookListId != null)
                        {
                            var ublWantToRead = _context.UserBookLists.Where(b => b.BookId == bookId).Where(b => b.BookListId == wantToReadBookListId.BookListId).FirstOrDefault();

                            if (ublWantToRead != null)
                            {
                                _context.UserBookLists.Remove(ublWantToRead);
                                _context.SaveChanges();
                            }

                        }

                        TempData["openModal"] = "Open Modal";
                        return RedirectToAction("BookDetails", new { id = bookId });
                    }
                }
            }
        }

        [HttpGet]
        public IActionResult EditList(int id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.Where(x => x.LoginUserId.Equals(currentUserId)).FirstOrDefault();

            var model = new BooksModel
            {
                WantToRead = _booksService.GetBooksInWantToRead(user.UserId),
                CurrentlyReading = _booksService.GetBooksInCurrentlyReading(user.UserId),
                AlreadyRead = _booksService.GetBooksInAlreadyRead(user.UserId)
            };

            if (id == 1) { ViewBag.Message = "Edit -Want to Read- List"; ViewBag.ID = 1; }
            else if (id == 2) { ViewBag.Message = "Edit -Currently Reading- List"; ViewBag.ID = 2; }
            else if (id == 3) { ViewBag.Message = "Edit -Already Read- List"; ViewBag.ID = 3; }
            else { return RedirectToAction("Error", "Home"); }

            return View(model);
        }

        [HttpPost]
        public IActionResult EditList(BooksModel model)
        {
            if(model.BooksSelected == null || model.BooksSelected.Count== 0)
            {
                string msg = "You must select at least one book to edit list";
                return RedirectToAction("Index", "Home", new { Msg = msg });
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.Where(x => x.LoginUserId.Equals(currentUserId)).FirstOrDefault();

            var lists = new BooksModel
            {
                WantToRead = _booksService.GetBooksInWantToRead(user.UserId),
                CurrentlyReading = _booksService.GetBooksInCurrentlyReading(user.UserId),
                AlreadyRead = _booksService.GetBooksInAlreadyRead(user.UserId)
            };

            foreach (var bookSelected in model.BooksSelected)
            {
                var book = _booksService.GetBookById(bookSelected);
                int bookListId = 0;

                foreach(var list in lists.WantToRead)
                {
                    if(list.BookId == bookSelected) { bookListId = 1;  }
                }
                if(bookListId == 0)
                {
                    foreach (var list in lists.CurrentlyReading)
                    {
                        if (list.BookId == bookSelected) { bookListId = 2; }
                    }
                    if (bookListId == 0)
                    {
                        foreach (var list in lists.AlreadyRead)
                        {
                            if (list.BookId == bookSelected) { bookListId = 3; }
                        }
                    }
                }
                
                
                if (bookListId == 1)
                {
                    var wantToReadBookListId = _context.BookLists.Where(x => x.UserId.Equals(user.UserId)).Where(y => y.BookListName.Equals("Want to read")).FirstOrDefault();
                    var ublWantToRead = _context.UserBookLists.Where(b => b.BookId == bookSelected).Where(b => b.BookListId == wantToReadBookListId.BookListId).FirstOrDefault();

                    if (wantToReadBookListId != null && ublWantToRead != null)
                    {
                        _context.UserBookLists.Remove(ublWantToRead);
                        _context.SaveChanges();
                    }
                }
                else if (bookListId == 2)
                {
                    var bookList = _context.BookLists.Where(x => x.UserId.Equals(user.UserId)).Where(y => y.BookListName.Equals("Already read")).FirstOrDefault();
                    UserBookLists ubl = new UserBookLists { BookListId = bookList.BookListId, BookId = bookSelected };

                    _context.UserBookLists.Add(ubl);
                    _context.SaveChanges();

                    var currentlyReadingListId = _context.BookLists.Where(x => x.UserId.Equals(user.UserId)).Where(y => y.BookListName.Equals("Currently reading")).FirstOrDefault();

                    if (currentlyReadingListId != null)
                    {
                        var ublCurrentlyReading = _context.UserBookLists.Where(b => b.BookId == bookSelected).Where(b => b.BookListId == currentlyReadingListId.BookListId).FirstOrDefault();

                        if (ublCurrentlyReading != null)
                        {
                            _context.UserBookLists.Remove(ublCurrentlyReading);
                            _context.SaveChanges();
                        }

                    }
                }
                else if (bookListId == 3)
                {
                    var alreadyReadBookListId = _context.BookLists.Where(x => x.UserId.Equals(user.UserId)).Where(y => y.BookListName.Equals("Already read")).FirstOrDefault();
                    var ublAlreadyRead = _context.UserBookLists.Where(b => b.BookId == bookSelected).Where(b => b.BookListId == alreadyReadBookListId.BookListId).FirstOrDefault();

                    if (alreadyReadBookListId != null && ublAlreadyRead != null)
                    {
                        _context.UserBookLists.Remove(ublAlreadyRead);
                        _context.SaveChanges();
                    }
                }

            }

            return RedirectToAction("Index", "Home");

        }



    }
}
