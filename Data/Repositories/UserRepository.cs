using DriverLicence.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace DriverLicence.Data.Repositories
{

    public interface IUserRepository
    {
        Task<User> GetById(int userId);
        Task<User> GetByUsername(string username);
        Task<User> GetByNationalNumber(string nationalNumber);
        Task<IEnumerable<User>> GetAll();
        Task<IEnumerable<User>> GetAllActive();
        Task<IEnumerable<User>> GetByRole(string role);
        Task Add(User user);
        void Update(User user);
        void Delete(User user);
        Task<bool> UserExists(int userId);
        Task<bool> UsernameExists(string username);
    }


    public class UserRepository:IUserRepository
    {

        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<User?> GetById(int userId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<User?> GetByUsername(string username)
        {
            return await _context.Users
               
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User?> GetByNationalNumber(string nationalNumber)
        {
            return await _context.Users
               
                .FirstOrDefaultAsync(u => u.NationalNumber == nationalNumber);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllActive()
        {
            return await _context.Users
                .Where(u => u.Active)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetByRole(string role)
        {
            return await _context.Users
                .Where(u => u.Role == role)
                .ToListAsync();
        }

        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public void Update(User user)
        {
         
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }

        public async Task<bool> UserExists(int userId)
        {
            return await _context.Users.AnyAsync(u => u.UserId == userId);
        }

        public async Task<bool> UsernameExists(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }
        

    }
}
