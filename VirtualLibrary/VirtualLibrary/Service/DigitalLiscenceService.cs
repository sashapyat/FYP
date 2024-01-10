using Core.Persistence.Context;
using VirtualLibrary.Models;

namespace VirtualLibrary.Service
{
    public class DigitalLiscenceService : IDigitalLiscenceService
    {
        private readonly AppDbContext _appDbContext;
        public DigitalLiscenceService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public DigitalLicenceModel GetDigitalLicenceByUserId(int userId)
        {
            DigitalLicenceModel foundLicence =
                (from l in _appDbContext.DigitalLicence
                 where l.UserId == userId
                 select new DigitalLicenceModel
                 {
                     LicenceId = l.LicenceId,
                     UserId = userId,
                     LicenceKey = l.LicenceKey,
                     LicenceXml = l.LicenceXml,
                     PublicKey = l.PublicKey
                 }).SingleOrDefault();

            return foundLicence;
        }

    }
}
