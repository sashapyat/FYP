using Core.Persistence.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualLibrary.Models
{
    public class BooksModel
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PublicationYear { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string Summary { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile BookCover { get; set; }
        public string BookCoverUrl { get; set; }
        public List<GenreModel> Genres { get; set; }

        public IEnumerable<BooksModel> WantToRead { get; set; }
        public IEnumerable<BooksModel> CurrentlyReading { get; set; }
        public IEnumerable<BooksModel> AlreadyRead { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile BookPdf { get; set; }
        public string BookPdfUrl { get; set; }

        public bool IsSelected { get; set; }
        public List<int> BooksSelected { get; set; }

    }
}
