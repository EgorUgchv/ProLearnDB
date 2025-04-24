using ProLearnDB.Dto;
using ProLearnDB.Models;

namespace ProLearnDB.Interfaces;

public interface IUserRepository
{
     bool CreateUser(User user);
    ICollection<User> GetUsers();
    bool Save();
}