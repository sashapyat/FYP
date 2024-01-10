using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Entities
{
    public class BookLists
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookListId { get; set; }
        public string BookListName { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }
    }
}
