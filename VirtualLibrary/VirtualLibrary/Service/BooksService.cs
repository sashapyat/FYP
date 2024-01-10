using Core.Persistence.Context;
using Core.Persistence.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using VirtualLibrary.Models;

namespace VirtualLibrary.Service
{
    public class BooksService : IBooksService
    {
        private readonly AppDbContext _appDbContext;
        public BooksService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public List<BooksModel> GetAllBooks()
        {
            var foundBooks =
                (from b in _appDbContext.Books
                 join a in _appDbContext.Authors on b.AuthorId equals a.AuthorId
                 select new BooksModel
                 {
                     BookId = b.BookId,
                     BookTitle = b.BookTitle,
                     PublicationYear = b.PublicationYear,
                     AuthorFirstName = a.AuthorFirstName,
                     AuthorLastName = a.AuthorLastName,
                     Summary = b.Summary,
                     BookCoverUrl = b.BookCover,
                     Genres = (from g in _appDbContext.Genres
                               join gInB in _appDbContext.GenresInBook on g.GenreId equals gInB.GenreId
                               where b.BookId == gInB.BookId
                               select new GenreModel { GenreName = g.GenreName }).ToList()
                 }).ToList();


            return foundBooks;

        }

        public List<GenreModel> GetAllGenres()
        {
            var foundGenres = 
                (from g in _appDbContext.Genres
                 select new GenreModel
                 {
                     GenreId = g.GenreId,
                     GenreName = g.GenreName,
                 }).ToList();

            return foundGenres;
        }

        public GenreModel GetGenreById(int id)
        {
            var foundGenres =
                (from g in _appDbContext.Genres
                 where g.GenreId == id
                 select new GenreModel
                 {
                     GenreId = g.GenreId,
                     GenreName = g.GenreName,
                 }).FirstOrDefault();

            return foundGenres;
        }

        public SelectList GetAllGenresSelectList()
        {

            var genres = new SelectList((
                from g in _appDbContext.Genres
                select new 
                {
                    g.GenreId,
                     g.GenreName,
                }).ToList(), "GenreId", "GenreName");
            

            return genres;
        }

        public BooksModel GetBookById(int id)
        {
            BooksModel foundBook =
                (from b in _appDbContext.Books
                 join a in _appDbContext.Authors on b.AuthorId equals a.AuthorId
                 where b.BookId == id
                 select new BooksModel
                 {
                     BookId = b.BookId,
                     BookTitle = b.BookTitle,
                     PublicationYear = b.PublicationYear,
                     AuthorFirstName = a.AuthorFirstName,
                     AuthorLastName = a.AuthorLastName,
                     Summary = b.Summary,
                     BookCoverUrl = b.BookCover,
                     Genres = (from g in _appDbContext.Genres
                               join gInB in _appDbContext.GenresInBook on g.GenreId equals gInB.GenreId
                               where b.BookId == gInB.BookId
                               select new GenreModel { GenreName = g.GenreName }).ToList(),
                     BookPdfUrl = b.BookPdfUrl
                 }).SingleOrDefault();

            return foundBook;
        }


        public List<BooksModel> GetBooksByGenre(int genreId)
        {
                var foundBooks =
                (from b in _appDbContext.Books
                 join a in _appDbContext.Authors on b.AuthorId equals a.AuthorId
                 join gInB in _appDbContext.GenresInBook on b.BookId equals gInB.BookId
                 join g in _appDbContext.Genres on gInB.GenreId equals g.GenreId
                 where g.GenreId == genreId
                 select new BooksModel
                 {
                     BookId = b.BookId,
                     BookTitle = b.BookTitle,
                     PublicationYear = b.PublicationYear,
                     AuthorFirstName = a.AuthorFirstName,
                     AuthorLastName = a.AuthorLastName,
                     Summary = b.Summary,
                     BookCoverUrl = b.BookCover,
                     Genres = (from g in _appDbContext.Genres
                               join gInB in _appDbContext.GenresInBook on g.GenreId equals gInB.GenreId
                               where b.BookId == gInB.BookId
                               select new GenreModel { GenreName = g.GenreName }).ToList()
                 }).ToList();

            if(foundBooks.Count == 0)
            {
                return null;
            }
            else
            {
                return foundBooks;
            }
        }

        public List<BooksModel> GetBooksInWantToRead(int userId)
        {
            var foundBooks =
                (from b in _appDbContext.Books
                 join a in _appDbContext.Authors on b.AuthorId equals a.AuthorId
                 join ubl in _appDbContext.UserBookLists on b.BookId equals ubl.BookId
                 join bl in _appDbContext.BookLists on ubl.BookListId equals bl.BookListId
                 where bl.UserId == userId && bl.BookListName == "Want to read"
                 select new BooksModel
                 {
                     BookId = b.BookId,
                     BookCoverUrl = b.BookCover,
                     BookTitle = b.BookTitle
                 }).ToList();

            return foundBooks;
        }

        public List<BooksModel> GetBooksInCurrentlyReading(int userId)
        {
            var foundBooks =
                (from b in _appDbContext.Books
                 join a in _appDbContext.Authors on b.AuthorId equals a.AuthorId
                 join ubl in _appDbContext.UserBookLists on b.BookId equals ubl.BookId
                 join bl in _appDbContext.BookLists on ubl.BookListId equals bl.BookListId
                 where bl.UserId == userId && bl.BookListName == "Currently reading"
                 select new BooksModel
                 {
                     BookId = b.BookId,
                     BookCoverUrl = b.BookCover,
                     BookTitle = b.BookTitle
                 }).ToList();

            return foundBooks;
        }

        public List<BooksModel> GetBooksInAlreadyRead(int userId)
        {
            var foundBooks =
                (from b in _appDbContext.Books
                 join a in _appDbContext.Authors on b.AuthorId equals a.AuthorId
                 join ubl in _appDbContext.UserBookLists on b.BookId equals ubl.BookId
                 join bl in _appDbContext.BookLists on ubl.BookListId equals bl.BookListId
                 where bl.UserId == userId && bl.BookListName == "Already read"
                 select new BooksModel
                 {
                     BookId = b.BookId,
                     BookCoverUrl = b.BookCover,
                     BookTitle = b.BookTitle
                 }).ToList();

            return foundBooks;
        }


    }
}
