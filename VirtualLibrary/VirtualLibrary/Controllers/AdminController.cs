using Core.Persistence.Context;
using Core.Persistence.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Security;
using System.Diagnostics.Metrics;
using System.Security.Claims;
using System.Security.Policy;
using VirtualLibrary.Models;
using VirtualLibrary.Service;

namespace VirtualLibrary.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly IBooksService _booksService;
        private readonly IUserDataService _userDataService;
        private readonly IAuthorsService _authorsService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly DbContextOptions<AppDbContext> _contextOptions;
        private readonly IConfiguration _configuration;
        private string _connectionString;
        DbContextOptionsBuilder<AppDbContext> _optionsBuilder;
        public AdminController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration,
            AppDbContext context,
            IBooksService booksService,
            IUserDataService userDataService,
            IAuthorsService authorsService,
            IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _configuration = configuration;
            _optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            _connectionString = _configuration.GetConnectionString("AppDbConnection");
            _optionsBuilder.UseSqlServer(_connectionString);
            _booksService = booksService;
            _userDataService = userDataService;
            _authorsService = authorsService;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UserAdministration()
        {
            var allUsers = new UsersAndMembersModel
            {
                RegisteredUsers = _userDataService.GetAllRegisteredUsers(),
                Members = _userDataService.GetAllMembers()
            };
            
            return View(allUsers);
        }

        [HttpGet]
        public IActionResult UserDetails(int id)
        {
            var model = new UsersAndMembersModel
            {
                User = _userDataService.GetUserById(id),
                Member = _userDataService.GetMemberById(id)
            };

            return View(model);


        }

        [HttpGet]
        public IActionResult BookAdministration()
        {

            return View();
        }

        [HttpGet]
        public IActionResult AddBook(string Msg)
        {
            ViewBag.Message = Msg;
            var model = new BooksGenresAuthorsModel
            {
                AuthorsList = _authorsService.GetAllAuthors(),
                GenresList = _booksService.GetAllGenres()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddBookAsync(BooksGenresAuthorsModel model, GenreModel genreModel, string authorId, IFormFile BookCover, IFormFile BookPdf, int PublicationYear)
        {
            var model2 = new BooksGenresAuthorsModel
            {
                AuthorsList = _authorsService.GetAllAuthors(),
                GenresList = _booksService.GetAllGenres()
            };

            SelectList authors = model2.AuthorsList;
            SelectListItem selectItem = authors.ToList().Find(x => x.Value == authorId);

            DateTime pubYear = new DateTime(PublicationYear, 01, 01);

            if (selectItem == null && (model.Book.AuthorFirstName != null && model.Book.AuthorLastName != null))
            {
                string wwwPath = _webHostEnvironment.WebRootPath;
                string contentPath = _webHostEnvironment.ContentRootPath;

                string pathImg = Path.Combine(_webHostEnvironment.WebRootPath, "images\\bookCovers");
                if (!Directory.Exists(pathImg))
                {
                    Directory.CreateDirectory(pathImg);
                }
                var fileNameBookCover = Path.GetFileName(BookCover.FileName);
                FileStream fileStreamImg = new FileStream(Path.Combine(pathImg, fileNameBookCover), FileMode.Create);
                
                using (fileStreamImg)
                {
                    await BookCover.CopyToAsync(fileStreamImg);
                }

                byte[] byteValue = System.Text.ASCIIEncoding.ASCII.GetBytes(Path.GetFileNameWithoutExtension(BookPdf.FileName));
                string encrypted = Convert.ToBase64String(byteValue);

                var fileNameBookPdf = "Protected_" + encrypted;
                string fileextention = Path.GetExtension(BookPdf.FileName);
                string filePdf = fileNameBookPdf + fileextention;

                string pathPdf = Path.Combine(_webHostEnvironment.WebRootPath, "bookPdf");
                if (!Directory.Exists(pathPdf))
                {
                    Directory.CreateDirectory(pathPdf);
                }

                using (var stream = new MemoryStream())
                {
                    //
                    // code from: https://help.syncfusion.com/cr/file-formats/Syncfusion.Pdf.Parsing.PdfLoadedDocument.html
                    // https://www.syncfusion.com/blogs/post/simple-steps-to-encrypt-and-decrypt-pdf-files-using-c.aspx#decrypt-pdf-document
                    //

                    await BookPdf.CopyToAsync(stream);
                    stream.Seek(0, SeekOrigin.Begin);

                    byte[] pdfData = new byte[stream.Length];
                    stream.Read(pdfData, 0, System.Convert.ToInt32(pdfData.Length));
                    PdfLoadedDocument document = new PdfLoadedDocument(pdfData);

                    PdfSecurity security = document.Security;

                    security.OwnerPassword = "pass";

                    security.Algorithm = PdfEncryptionAlgorithm.AES;
                    security.KeySize = PdfEncryptionKeySize.Key256Bit;

                    security.EncryptionOptions = PdfEncryptionOptions.EncryptAllContentsExceptMetadata;

                    Stream newFile = new FileStream(Path.Combine(pathPdf, filePdf), FileMode.Create);
                    document.Save(newFile);
                    document.Close(true);

                    stream.Close();
                    newFile.Close();
                }                

                Authors a = new Authors
                {
                    AuthorFirstName = model.Book.AuthorFirstName,
                    AuthorLastName = model.Book.AuthorLastName
                };

                _context.Authors.Add(a);
                _context.SaveChanges();

                Books b = new Books
                {
                    BookTitle = model.Book.BookTitle,
                    PublicationYear = pubYear,
                    AuthorId = a.AuthorId,
                    Summary = model.Book.Summary,
                    BookCover = fileNameBookCover,
                    BookPdfUrl = filePdf
                };

                _context.Books.Add(b);
                _context.SaveChanges();

                foreach (var genreSelected in genreModel.GenresSelected)
                {
                    var genre = _booksService.GetGenreById(genreSelected);

                    GenresInBook g = new GenresInBook
                    {
                        BookId = b.BookId,
                        GenreId = genre.GenreId
                    };

                    _context.GenresInBook.Add(g);
                    _context.SaveChanges();

                }


            }
            else if (selectItem != null && (model.Book.AuthorFirstName == null && model.Book.AuthorLastName == null))
            {

                string wwwPath = _webHostEnvironment.WebRootPath;
                string contentPath = _webHostEnvironment.ContentRootPath;

                string pathImg = Path.Combine(_webHostEnvironment.WebRootPath, "images\\bookCovers");
                if (!Directory.Exists(pathImg))
                {
                    Directory.CreateDirectory(pathImg);
                }
                var fileNameBookCover = Path.GetFileName(BookCover.FileName);
                FileStream fileStreamImg = new FileStream(Path.Combine(pathImg, fileNameBookCover), FileMode.Create);

                using (fileStreamImg)
                {
                    await BookCover.CopyToAsync(fileStreamImg);
                }

                byte[] byteValue = System.Text.ASCIIEncoding.ASCII.GetBytes(Path.GetFileNameWithoutExtension(BookPdf.FileName));
                string encrypted = Convert.ToBase64String(byteValue);

                var fileNameBookPdf = "Protected_" + encrypted;
                string fileextention = Path.GetExtension(BookPdf.FileName);
                string filePdf = fileNameBookPdf + fileextention;

                string pathPdf = Path.Combine(_webHostEnvironment.WebRootPath, "bookPdf");
                if (!Directory.Exists(pathPdf))
                {
                    Directory.CreateDirectory(pathPdf);
                }

                using (var stream = new MemoryStream())
                {
                    //
                    // code from: https://help.syncfusion.com/cr/file-formats/Syncfusion.Pdf.Parsing.PdfLoadedDocument.html
                    // https://www.syncfusion.com/blogs/post/simple-steps-to-encrypt-and-decrypt-pdf-files-using-c.aspx#decrypt-pdf-document
                    //
                    await BookPdf.CopyToAsync(stream);
                    stream.Seek(0, SeekOrigin.Begin);

                    byte[] pdfData = new byte[stream.Length];
                    stream.Read(pdfData, 0, System.Convert.ToInt32(pdfData.Length));
                    PdfLoadedDocument document = new PdfLoadedDocument(pdfData);

                    PdfSecurity security = document.Security;

                    security.OwnerPassword = "pass";

                    security.Algorithm = PdfEncryptionAlgorithm.AES;
                    security.KeySize = PdfEncryptionKeySize.Key256Bit;

                    security.EncryptionOptions = PdfEncryptionOptions.EncryptAllContentsExceptMetadata;

                    Stream newFile = new FileStream(Path.Combine(pathPdf, filePdf), FileMode.Create);
                    document.Save(newFile);
                    document.Close(true);

                    stream.Close();
                    newFile.Close();
                }

                var author = _authorsService.GetAuthorById(Int32.Parse(authorId));
                Books b = new Books
                {
                    BookTitle = model.Book.BookTitle,
                    PublicationYear = pubYear,
                    AuthorId = author.AuthorId,
                    Summary = model.Book.Summary,
                    BookCover = fileNameBookCover,
                    BookPdfUrl = filePdf
                };

                _context.Books.Add(b);
                _context.SaveChanges();

                foreach (var genreSelected in genreModel.GenresSelected)
                {
                    var genre = _booksService.GetGenreById(genreSelected);

                    GenresInBook g = new GenresInBook
                    {
                        BookId = b.BookId,
                        GenreId = genre.GenreId
                    };

                    _context.GenresInBook.Add(g);
                    _context.SaveChanges();

                }

            }
            else
            {
                string errormsg = "You must either select an existing author or create a new author entry. Not both.";
                return RedirectToAction("AddBook", new { Msg = errormsg });
            }



            string msg = "New book added";
            return RedirectToAction("Index", "Home", new { Msg = msg });
        }

        [HttpGet]
        public IActionResult EditBook(string Msg, int id)
        {
            var book = _booksService.GetBookById(id);
            var a = _context.Authors.Where(x => x.AuthorFirstName == book.AuthorFirstName && x.AuthorLastName == book.AuthorLastName).FirstOrDefault();
            ViewBag.Message = Msg;
            var model = new BooksGenresAuthorsModel
            {
                AuthorsList = _authorsService.GetAllAuthors(),
                GenresList = _booksService.GetAllGenres(),
                Book = book,
                Author = new AuthorsModel
                {
                    AuthorId = a.AuthorId, AuthorFirstName = a.AuthorFirstName, AuthorLastName = a.AuthorLastName
                }
            };

            foreach(var g in model.GenresList)
            {
                foreach(var b in book.Genres)
                {
                    if(b.GenreName == g.GenreName) { g.IsSelected = true; }
                }
            }

            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditBookAsync(BooksGenresAuthorsModel model, GenreModel genreModel, string authorId, IFormFile BookCover, IFormFile BookPdf, int PublicationYear)
        {

            var model2 = new BooksGenresAuthorsModel
            {
                Book = model.Book,
                AuthorsList = _authorsService.GetAllAuthors(),
                GenresList = _booksService.GetAllGenres()
            };
            var book = _context.Books.Where(x => x.BookId == model.Book.BookId).FirstOrDefault();
            var genresInBook = _context.GenresInBook.Where(x => x.BookId == model.Book.BookId).ToList();
            SelectList authors = model2.AuthorsList;
            SelectListItem selectItem = authors.ToList().Find(x => x.Value == authorId);

            DateTime pubYear = new DateTime(PublicationYear, 01, 01);

            if (selectItem == null && (model.Book.AuthorFirstName != null && model.Book.AuthorLastName != null))
            {
                string wwwPath = _webHostEnvironment.WebRootPath;
                string contentPath = _webHostEnvironment.ContentRootPath;

                if(BookCover != null)
                {
                    string pathImg = Path.Combine(_webHostEnvironment.WebRootPath, "images\\bookCovers");
                    if (!Directory.Exists(pathImg))
                    {
                        Directory.CreateDirectory(pathImg);
                    }
                    var fileNameBookCover = Path.GetFileName(BookCover.FileName);
                    FileStream fileStreamImg = new FileStream(Path.Combine(pathImg, fileNameBookCover), FileMode.Create);

                    using (fileStreamImg)
                    {
                        await BookCover.CopyToAsync(fileStreamImg);
                    }
                    book.BookCover = fileNameBookCover;
                }

                if(BookPdf != null)
                {
                    byte[] byteValue = System.Text.ASCIIEncoding.ASCII.GetBytes(Path.GetFileNameWithoutExtension(BookPdf.FileName));
                    string encrypted = Convert.ToBase64String(byteValue);

                    var fileNameBookPdf = "Protected_" + encrypted;
                    string fileextention = Path.GetExtension(BookPdf.FileName);
                    string filePdf = fileNameBookPdf + fileextention;

                    string pathPdf = Path.Combine(_webHostEnvironment.WebRootPath, "bookPdf");
                    if (!Directory.Exists(pathPdf))
                    {
                        Directory.CreateDirectory(pathPdf);
                    }

                    using (var stream = new MemoryStream())
                    {
                        //
                        // code from: https://help.syncfusion.com/cr/file-formats/Syncfusion.Pdf.Parsing.PdfLoadedDocument.html
                        // https://www.syncfusion.com/blogs/post/simple-steps-to-encrypt-and-decrypt-pdf-files-using-c.aspx#decrypt-pdf-document
                        //

                        await BookPdf.CopyToAsync(stream);
                        stream.Seek(0, SeekOrigin.Begin);

                        byte[] pdfData = new byte[stream.Length];
                        stream.Read(pdfData, 0, System.Convert.ToInt32(pdfData.Length));
                        PdfLoadedDocument document = new PdfLoadedDocument(pdfData);

                        PdfSecurity security = document.Security;

                        security.OwnerPassword = "pass";

                        security.Algorithm = PdfEncryptionAlgorithm.AES;
                        security.KeySize = PdfEncryptionKeySize.Key256Bit;

                        security.EncryptionOptions = PdfEncryptionOptions.EncryptAllContentsExceptMetadata;

                        Stream newFile = new FileStream(Path.Combine(pathPdf, fileNameBookPdf), FileMode.Create);
                        document.Save(newFile);
                        document.Close(true);

                        stream.Close();
                        newFile.Close();
                    }

                    book.BookPdfUrl = filePdf;
                }

                Authors a = new Authors
                {
                    AuthorFirstName = model.Book.AuthorFirstName,
                    AuthorLastName = model.Book.AuthorLastName
                };

                _context.Authors.Add(a);
                _context.SaveChanges();

            }
            else if(selectItem != null && (model.Book.AuthorFirstName == null && model.Book.AuthorLastName == null))
            {
                string wwwPath = _webHostEnvironment.WebRootPath;
                string contentPath = _webHostEnvironment.ContentRootPath;

                if (BookCover != null)
                {
                    string pathImg = Path.Combine(_webHostEnvironment.WebRootPath, "images\\bookCovers");
                    if (!Directory.Exists(pathImg))
                    {
                        Directory.CreateDirectory(pathImg);
                    }
                    var fileNameBookCover = Path.GetFileName(BookCover.FileName);
                    FileStream fileStreamImg = new FileStream(Path.Combine(pathImg, fileNameBookCover), FileMode.Create);

                    using (fileStreamImg)
                    {
                        await BookCover.CopyToAsync(fileStreamImg);
                    }
                    book.BookCover = fileNameBookCover;
                }

                if (BookPdf != null)
                {
                    byte[] byteValue = System.Text.ASCIIEncoding.ASCII.GetBytes(Path.GetFileNameWithoutExtension(BookPdf.FileName));
                    string encrypted = Convert.ToBase64String(byteValue);

                    var fileNameBookPdf = "Protected_" + encrypted;
                    string fileextention = Path.GetExtension(BookPdf.FileName);
                    string filePdf = fileNameBookPdf + fileextention;

                    string pathPdf = Path.Combine(_webHostEnvironment.WebRootPath, "bookPdf");
                    if (!Directory.Exists(pathPdf))
                    {
                        Directory.CreateDirectory(pathPdf);
                    }

                    using (var stream = new MemoryStream())
                    {
                        //
                        // code from: https://help.syncfusion.com/cr/file-formats/Syncfusion.Pdf.Parsing.PdfLoadedDocument.html
                        // https://www.syncfusion.com/blogs/post/simple-steps-to-encrypt-and-decrypt-pdf-files-using-c.aspx#decrypt-pdf-document
                        //

                        await BookPdf.CopyToAsync(stream);
                        stream.Seek(0, SeekOrigin.Begin);

                        byte[] pdfData = new byte[stream.Length];
                        stream.Read(pdfData, 0, System.Convert.ToInt32(pdfData.Length));
                        PdfLoadedDocument document = new PdfLoadedDocument(pdfData);

                        PdfSecurity security = document.Security;

                        security.OwnerPassword = "pass";

                        security.Algorithm = PdfEncryptionAlgorithm.AES;
                        security.KeySize = PdfEncryptionKeySize.Key256Bit;

                        security.EncryptionOptions = PdfEncryptionOptions.EncryptAllContentsExceptMetadata;

                        Stream newFile = new FileStream(Path.Combine(pathPdf, fileNameBookPdf), FileMode.Create);
                        document.Save(newFile);
                        document.Close(true);

                        stream.Close();
                        newFile.Close();
                    }
                    book.BookPdfUrl = filePdf;
                }

                var author = _authorsService.GetAuthorById(Int32.Parse(authorId));
                book.AuthorId = author.AuthorId;

            }
            else
            {
                string errormsg = "You must either select an existing author or create a new author entry. Not both.";
                return RedirectToAction("EditBook", new { Msg = errormsg });
            }

            book.BookTitle = model.Book.BookTitle;
            book.PublicationYear = pubYear;
            book.Summary = model.Book.Summary;

            foreach(var gen in genresInBook)
            {
                _context.Remove(gen);
            }

            foreach (var genreSelected in genreModel.GenresSelected)
            {
                var genre = _booksService.GetGenreById(genreSelected);

                GenresInBook g = new GenresInBook
                {
                    BookId = book.BookId,
                    GenreId = genre.GenreId
                };

                _context.GenresInBook.Add(g);
                _context.SaveChanges();

            }

            string msg = "Book successfully updated";
            return RedirectToAction("Index", "Home", new { Msg = msg });

        }

        [HttpGet]
        public IActionResult DeleteBook(int id, string Msg)
        {
            ViewBag.Message = Msg;
            var model = _booksService.GetBookById(id);

            if (model == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.Where(x => x.BookId == id).FirstOrDefault();
            string pathPdf = Path.Combine(_webHostEnvironment.WebRootPath, "bookPdf", book.BookPdfUrl);
            string pathImg = Path.Combine(_webHostEnvironment.WebRootPath, "images\\bookCovers", book.BookCover);
            if (System.IO.File.Exists(pathPdf) && System.IO.File.Exists(pathImg))
            {
                System.IO.File.Delete(pathPdf);
                System.IO.File.Delete(pathImg);
            }

            _context.Books.Remove(book);
            _context.SaveChanges();

            string msg = "Book successfully deleted";
            return RedirectToAction("Index", "Home", new { Msg = msg });
        }

    }
}
