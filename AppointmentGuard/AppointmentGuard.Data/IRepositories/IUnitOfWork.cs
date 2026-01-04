namespace AppointmentGuard.Data.IRepositories
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        void Commit();      
    }
}