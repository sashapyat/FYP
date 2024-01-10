using Microsoft.AspNetCore.Mvc.Rendering;

namespace VirtualLibrary.Models
{
    public class BooksAndGenresModel
    {
        public IEnumerable<BooksModel> Books { get; set; }
        public IEnumerable<GenreModel> Genres { get; set; }
        public SelectList GenresList { get; set; }
    }
}
