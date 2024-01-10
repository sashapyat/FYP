namespace VirtualLibrary.Models
{
    public class UsersAndMembersModel
    {
        public IEnumerable<UserDetailsModel> RegisteredUsers { get; set; }
        public IEnumerable<UserDetailsModel> Members { get; set; }

        public MemberModel Member { get; set; }
        public UserDetailsModel User { get; set; }

    }
}
