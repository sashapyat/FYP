using Core.Persistence.Entities;
using VirtualLibrary.Models;

namespace VirtualLibrary.Service
{
    public interface IUserDataService
    {
        Task<Users?> GetUserByLoginUserId(string userId);
        public List<UserDetailsModel> GetAllRegisteredUsers();
        public List<UserDetailsModel> GetAllMembers();
        public MemberModel GetMemberById(int id);
        public UserDetailsModel GetUserById(int id);
    }
}
