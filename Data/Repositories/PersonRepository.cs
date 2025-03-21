using DriverLicence.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace DriverLicence.Data.Repositories
{

    public interface IPersonRepository
    {

        Task<Person> GetByNationalNumber(string nationalNumber);
        Task<Person> GetByPhoneNumber(string phoneNumber);
        Task<Person> GetByFullName(string fullName);
        Task<Person> GetByEmail(string email);
        Task<IEnumerable<Person>> GetAll();
        Task<IEnumerable<Person>> GetByNationality(string nationalty);
        Task<IEnumerable<Person>> GetByGender(string gender);
        Task Add(Person person);
        void Update(Person person);
        void Delete(Person person);
        Task<bool> CheckExistByNationalNumber(string nationalNumber);
        Task<bool> CheckExistByEmail(string email);





    }


    public class PersonRepository:IPersonRepository
    {
        private readonly AppDbContext _context;
        public PersonRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Person?> GetByNationalNumber(string nationalNumber)
        {
            return await _context.Persons.FirstOrDefaultAsync(p => p.NationalNumber == nationalNumber);
        }

        public async Task<Person?> GetByPhoneNumber(string phoneNumber)
        {
            return await _context.Persons.FirstOrDefaultAsync(p => p.PhoneNumber == phoneNumber);
        }

        public async Task<Person?> GetByFullName(string fullName)
        {
            return await _context.Persons.FirstOrDefaultAsync(p => p.FullName == fullName);
        }


        public async Task<Person?> GetByEmail(string email)
        {
            return await _context.Persons.FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            return await _context.Persons.ToListAsync();
        }


       public async Task<IEnumerable<Person>> GetByNationality(string nationalty)
        {
            return await _context.Persons.Where(p => p.Nationality == nationalty).ToListAsync();
        }

        public async Task<IEnumerable<Person>> GetByGender(string gender)
        {
            return await _context.Persons.Where(p => p.Gender == gender).ToListAsync();
        }


        public async Task Add(Person person)
        {
            await _context.Persons.AddAsync(person);
        }

        public void Update(Person person)
        {
            
        }

        public void  Delete(Person person) {
              _context.Persons.Remove(person);
        }

       public async Task<bool> CheckExistByNationalNumber(string nationalNumber)
        {
            return await _context.Persons.AnyAsync(p => p.NationalNumber == nationalNumber);
           
        }
        public async Task<bool> CheckExistByEmail(string email)
        {
            return await _context.Persons.AnyAsync(p => p.Email == email);


        }
    }
}
