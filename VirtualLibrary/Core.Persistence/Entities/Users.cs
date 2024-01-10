using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Entities
{
    public class Users
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [StringLength(128, MinimumLength = 6)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        public string LoginUserId { get; set; }

    }
}
