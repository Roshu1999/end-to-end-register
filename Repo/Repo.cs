using IdentityCMS.Models;

namespace IdentityCMS.Repo
{
    public class Repo : IRepo
    {
        private readonly CustomDbContext _dbContext;
        private readonly IPasswordHasher _passwordHasher;


        public Repo(CustomDbContext c , IPasswordHasher passwordHasher)
        {
            _dbContext = c;
            _passwordHasher = passwordHasher;

        }
        public string createcustomer(registeruser reg)
        {
            _dbContext.registerCMS.Add(new usermodel
            {
                username = reg.username,
                email = reg.email,
                phone = reg.phone,
                password = _passwordHasher.GenerateIdentityV3Hash(reg.password),

            });
            _dbContext.SaveChanges();
            return "OK";
        }
    }  
}
