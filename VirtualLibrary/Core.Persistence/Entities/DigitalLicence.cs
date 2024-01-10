using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Entities
{
    public class DigitalLicence
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LicenceId { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }

        [Required]
        public string LicenceKey { get; set; }

        [Required]
        public string LicenceXml { get; set; }

        [Required]
        public string PublicKey { get; set; }

    }
}
