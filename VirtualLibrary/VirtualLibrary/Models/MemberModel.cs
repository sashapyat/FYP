using Core.Persistence.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualLibrary.Models
{
    public class MemberModel
    {
        public int MembershipId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int TotalBooks { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int UserId { get; set; }
        public string LoginUserId { get; set; }
        public string Customer { get; set; }
        public bool IsActive { get; set; }  
    }
}
