using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Entities
{
    public class UserBookLists
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserBookListId { get; set; }
        public int BookListId { get; set; }
        [ForeignKey("BookListId")]
        public virtual BookLists BookLists { get; set; }
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public virtual Books Books { get; set; }
    }
}
