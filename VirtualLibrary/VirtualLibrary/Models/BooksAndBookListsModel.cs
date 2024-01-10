namespace VirtualLibrary.Models
{
    public class BooksAndBookListsModel
    {
        public IEnumerable<BooksModel> Books { get; set; }
        public IEnumerable<BookListsModel> BookLists { get; set; }
    }
}
