using ProLearnDB.Dto;
using ProLearnDB.Models;

namespace ProLearnDB.Interfaces;

public interface IUserRepository
{
     bool CreateUser(User user);
    ICollection<User> GetUsers();
    User? GetUserByPhoneNumber(string phoneNumber);
    bool DeleteUser(User user);
    bool Save();
    bool UserExists(string phoneNumber);
    bool CheckChatIdExists(int chatId);
    User? GetUserByChatId(long chatId);
}