using A2.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace A2.Data
{
    public class A2Repo : IA2Repo
    {
        private readonly A2DbContext _dbContext;


        public A2Repo(A2DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _dbContext.Users.ToList<User>();
        }


        public bool IsUsernameUnique(string username)
        {
            User user = _dbContext.Users.FirstOrDefault(e => e.UserName == username);

            if (user == null)
            {
                return true;
            }

            return false;
        }


        public User AddUser(User user)
        {
            EntityEntry<User> e = _dbContext.Users.Add(user);
            User u = e.Entity;
            _dbContext.SaveChanges();

            return u;
        }


    }
}