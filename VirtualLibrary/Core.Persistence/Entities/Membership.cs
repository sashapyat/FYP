using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Entities
{
    public class Membership
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MembershipId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int TotalBooks { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }
        public string Customer { get; set; }
        public bool IsActive { get; set; }
    }
}
