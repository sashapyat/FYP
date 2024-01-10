using Microsoft.AspNetCore.Mvc.Rendering;

namespace VirtualLibrary.Models
{
    public class BooksGenresAuthorsModel
    {
        public BooksModel Book { get; set; }
        public AuthorsModel Author { get; set; }
        public SelectList AuthorsList { get; set; }
        public IEnumerable<GenreModel> GenresList { get; set; }

    }
}
