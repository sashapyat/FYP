using Core.Persistence.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualLibrary.Models
{
    public class DigitalLicenceModel
    {
        public int LicenceId { get; set; }

        public int UserId { get; set; }

        public string LicenceKey { get; set; }

        public string LicenceXml { get; set; }

        public string PublicKey { get; set; }
    }
}
