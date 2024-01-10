using Core.Persistence.Context;
using Core.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using VirtualLibrary.Models;

namespace VirtualLibrary.Service
{
    public class UserDataService : IUserDataService
    {
        private readonly AppDbContext _appDbContext;

        public UserDataService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Users?> GetUserByLoginUserId(string userId)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(x => x.LoginUserId.Equals(userId));
        }

        public List<UserDetailsModel> GetAllRegisteredUsers()
        {
            var foundUsers =
                (from u in _appDbContext.Users
                 where !(from u1 in _appDbContext.Users
                         join m1 in _appDbContext.Membership on u1.UserId equals m1.UserId
                         where u1.UserId == m1.UserId
                         select m1.UserId).Contains(u.UserId) &&
                         u.UserId != 17 //admin
                 select new UserDetailsModel
                 {
                     UserId= u.UserId, 
                     FirstName= u.FirstName,    
                     LastName= u.LastName,
                     Email= u.Email,
                     Username= u.Username,
                     TotalBooks= 0,
                     Role="Registered User",
                     LoginUserId =u.LoginUserId
                 }).ToList();


            return foundUsers;

        }

        public List<UserDetailsModel> GetAllMembers()
        {
            var foundUsers =
                (from u in _appDbContext.Users
                 join m in _appDbContext.Membership on u.UserId equals m.UserId
                 where u.UserId == m.UserId &&
                 m.IsActive == true
                 select new UserDetailsModel
                 {
                     UserId = u.UserId,
                     FirstName = u.FirstName,
                     LastName = u.LastName,
                     Email = u.Email,
                     Username = u.Username,
                     TotalBooks = m.TotalBooks,
                     Role = "Member",
                     LoginUserId = u.LoginUserId
                 }).ToList();


            return foundUsers;

        }

        public MemberModel GetMemberById(int id)
        {
            var foundUser = (from u in _appDbContext.Users
                                   join m in _appDbContext.Membership on u.UserId equals m.UserId
                                   where u.UserId == id &&
                                   m.IsActive == true
                                   orderby m.StartDate ascending
                                   select new MemberModel
                                   {
                                       UserId = u.UserId,
                                       FirstName = u.FirstName,
                                       LastName = u.LastName,
                                       Email = u.Email,
                                       Username = u.Username,
                                       TotalBooks = m.TotalBooks,
                                       Role = "Member",
                                       LoginUserId = u.LoginUserId,
                                       StartDate = m.StartDate,
                                       EndDate = m.EndDate,
                                       IsActive = m.IsActive
                                   }).LastOrDefault();

            if(foundUser == null)
            {
                return null;
            }
            else
            {
                return foundUser;
            }
            
        }

        public UserDetailsModel GetUserById(int id)
        {
            var foundUser = (from u in _appDbContext.Users
                            where u.UserId == id
                            select new UserDetailsModel
                            {
                                UserId = u.UserId,
                                FirstName = u.FirstName,
                                LastName = u.LastName,
                                Email = u.Email,
                                Username = u.Username,
                                Role = "Registered User"
                            }).FirstOrDefault();
            if (foundUser == null)
            {
                return null;
            }
            else
            {
                return foundUser;
            }
        }
    }
}
