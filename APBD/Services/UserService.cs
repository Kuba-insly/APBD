using APBD.Models.Users;

namespace APBD.Services;

public class UserService
{
    private readonly List<User> _users;

    public UserService(List<User> users)
    {
        _users = users;
    }

    public void AddUser(User user)
    {
        _users.Add(user);
    }
    
    public List<User> GetAllUsers()
    {
        return _users;
    }

    public User? GetUserById(Guid id)
    {
        return _users.FirstOrDefault(u => u.UserId == id);
    }
}