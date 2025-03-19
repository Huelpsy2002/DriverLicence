using DriverLicence.Data;

namespace DriverLicence.Business.Services
{

    public interface IUserService
    {



    }

    public class UserService:IUserService
    {
        private readonly IConfiguration _configration;
        private readonly AppDbContext _context;
        public UserService(AppDbContext context, IConfiguration configuration)
        {
            _configration = configuration;
            _context = context;
        }


    }
}
