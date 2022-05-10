using OTP.Models;

namespace OTP.Repository.IRepository
{
    public interface IOTPHistoryRepository : IRepository<OTPHistory>
    {
        void Update(OTPHistory oTP);
    }
}
