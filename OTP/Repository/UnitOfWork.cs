using OTP.Data;
using OTP.Repository.IRepository;

namespace OTP.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IOTPHistoryRepository OTPHistory { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            ApplicationUser = new ApplicationUserRepository(_db);
            OTPHistory = new OTPHistoryRepository(_db); 
        }


        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
