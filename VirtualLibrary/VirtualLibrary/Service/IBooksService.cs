using Microsoft.AspNetCore.Mvc.Rendering;
using VirtualLibrary.Models;

namespace VirtualLibrary.Service
{
    public interface IBooksService
    {
        public List<BooksModel> GetAllBooks();
        List<GenreModel> GetAllGenres();
        public SelectList GetAllGenresSelectList();
        public GenreModel GetGenreById(int id);
        public BooksModel GetBookById(int id);
        public List<BooksModel> GetBooksByGenre(int genreId);
        public List<BooksModel> GetBooksInWantToRead(int userId);
        public List<BooksModel> GetBooksInCurrentlyReading(int userId);
        public List<BooksModel> GetBooksInAlreadyRead(int userId);
    }
}
