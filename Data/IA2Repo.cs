using A2.Models;


namespace A2.Data
{
    public interface IA2Repo
    {
        public IEnumerable<User> GetAllUsers();

        public bool IsUsernameUnique(string username);

        public User AddUser(User user);

        public IEnumerable<Organizer> GetAllOrganizers();

        public List<string> GetAllOrganizerUsernames();

        public bool ValidLogin(string username, string password);

        public Sign GetSignById(string id);

        public User GetUserByUsername(string username);

        // delete this
        public Organizer AddOrganizer(Organizer organizer);

        public Event AddEvent(Event ev);

        public int GetEventCount();

        public Event GetEventById(int id);
    }
}

