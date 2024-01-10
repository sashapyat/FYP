using VirtualLibrary.Models;

namespace VirtualLibrary.Service
{
    public interface IDigitalLiscenceService
    {
        public DigitalLicenceModel GetDigitalLicenceByUserId(int userId);
    }
}
