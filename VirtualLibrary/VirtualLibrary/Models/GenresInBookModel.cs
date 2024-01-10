using Core.Persistence.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualLibrary.Models
{
    public class GenresInBookModel
    {
        public int GenresInBookId { get; set; }
        public int BookId { get; set; }
        public int GenreId { get; set; }
        public string GenreName { get; set; }
    }
}
