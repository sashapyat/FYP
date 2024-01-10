using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Core.Persistence.Entities
{
    public class Books
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        [Column(TypeName = "DATE")]
        public DateTime PublicationYear { get; set; }
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public virtual Authors Authors { get; set; }
        [Column(TypeName = "TEXT")]
        public string Summary { get; set; }
        public string BookCover { get; set; }
        public string BookPdfUrl { get; set; }

    }
}
