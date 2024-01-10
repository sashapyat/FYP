using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Entities
{
    public class GenresInBook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GenresInBookId { get; set; }
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public virtual Books Books { get; set; }
        public int GenreId { get; set; }
        [ForeignKey("GenreId")]
        public virtual Genres Genres { get; set; }
    }
}
