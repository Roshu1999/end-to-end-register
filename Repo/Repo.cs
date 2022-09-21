using IdentityCMS.Models;

namespace IdentityCMS.Repo
{
    public class Repo : IRepo
    {
        private readonly CustomDbContext _dbContext;


        public Repo(CustomDbContext c)
        {
            _dbContext = c;

        }
        public string createcustomer(registeruser reg)
        {
            _dbContext.registerCMS.Add(new usermodel
            {
                username = reg.username,
                email = reg.email,
                phone = reg.phone,
                password = reg.password,
            });
            _dbContext.SaveChanges();
            return "OK";
        }
    }  
}
