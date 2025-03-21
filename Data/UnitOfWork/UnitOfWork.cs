using DriverLicence.Data.Repositories;

namespace DriverLicence.Data.UnitOfWork
{
    // Interface
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IPersonRepository PersonRepository { get; } 


        Task<int> SaveChangesAsync();
        void Dispose();
    }

    // Implementation
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IUserRepository _userRepository;
        private IPersonRepository _personRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository
        {
            get { return _userRepository ??= new UserRepository(_context); }
        }
        public IPersonRepository PersonRepository
        {
            get { return _personRepository ??= new PersonRepository(_context); }
        }

        

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
