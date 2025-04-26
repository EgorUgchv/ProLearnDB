using ProLearnDB.Dto;
using ProLearnDB.Models;

namespace ProLearnDB.Interfaces;

public interface IUserRepository
{
     bool CreateUser(User user);
    ICollection<User> GetUsers();
    User? GetUserByPhoneNumber(string phoneNumber);
    bool Save();
    bool UserExists(string phoneNumber);
}