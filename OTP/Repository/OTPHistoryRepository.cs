using OTP.Data;
using OTP.Models;
using OTP.Repository.IRepository;

namespace OTP.Repository
{
    public class OTPHistoryRepository : Repository<OTPHistory>, IOTPHistoryRepository
    {
        private readonly ApplicationDbContext _db;
        public OTPHistoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OTPHistory oTP)
        {
            _db.OTPHistory.Update(oTP);
        }
    }
}
