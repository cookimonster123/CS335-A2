using A2.Models;


namespace A2.Data
{
    public interface IA2Repo
    {
        IEnumerable<User> GetAllUsers();

        bool IsUsernameUnique(string username);

        User AddUser(User user);
        
        

   
    }
}

