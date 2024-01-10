using Core.Persistence.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualLibrary.Models
{
    public class BookListsModel
    {
        public int BookListId { get; set; }
        public string BookListName { get; set; }
        public int UserBookListId { get; set; }
        public string UserBookListName { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
    }
}
