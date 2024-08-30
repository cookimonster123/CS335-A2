using A2.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;

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

        public IEnumerable<Organizer> GetAllOrganizers()
        {
            return _dbContext.Organizers.ToList<Organizer>();
        }

        public List<string> GetAllOrganizerUsernames()
        {
            List<string> usernames = new List<string>();
            foreach (Organizer u in _dbContext.Organizers)
            {
                usernames.Add(u.Name);
            }
            return usernames;
        }

        public bool ValidLogin(string username, string password)
        {
            // only checks the User DB
            User u = _dbContext.Users.FirstOrDefault(
                u => u.UserName == username && u.Password == password);

            Organizer o = _dbContext.Organizers.FirstOrDefault(
                o => o.Name == username && o.Password == password);

            if (u == null && o == null)
            {
                return false;
            }
            return true;
        }

        public Sign GetSignById(string id)
        {
            Sign sign = _dbContext.Signs.FirstOrDefault(e => e.Id == id);

            return sign;
        }

        public User GetUserByUsername(string username)
        {
            User user = _dbContext.Users.FirstOrDefault(e => e.UserName == username);

            return user;
        }

        // delete this
        public Organizer AddOrganizer(Organizer organizer)
        {
            EntityEntry<Organizer> e = _dbContext.Organizers.Add(organizer);
            Organizer u = e.Entity;
            _dbContext.SaveChanges();

            return u;
        }

        public Event AddEvent(Event ev)
        {
            EntityEntry<Event> e = _dbContext.Events.Add(ev);
            Event u = e.Entity;
            _dbContext.SaveChanges();

            return u;
        }

        public int GetEventCount()
        {
            List<Event> events = _dbContext.Events.ToList();
            return events.Count;
        }

        public Event GetEventById(int id)
        {
            Event e = _dbContext.Events.FirstOrDefault(c => c.Id == id);

            return e;
        }
    }
}