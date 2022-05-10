namespace OTP.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IApplicationUserRepository ApplicationUser { get; }
        IOTPHistoryRepository OTPHistory { get; }
        void Save();
    }
}
