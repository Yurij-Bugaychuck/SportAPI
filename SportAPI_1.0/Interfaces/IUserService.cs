using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportAPI.Models;
namespace SportAPI.Interfaces
{
    public interface IUserService
    {
        User GetByEmail(string email);
        User GetById(Guid id);

        Task<Guid> CreateUser(User user);

        Task<User> UpdateUser(User userDB, User user);
        Task<UserOption> AddUserOption(User userDB, UserOption option);
        Dictionary<string, string> GetUserOptions(User userDB);
        List<UserOption> GetUserOptionByKey(User user, string key);


        Task<UserStat> AddUserStat(User userDB, UserStat option);
        Dictionary<string, int> GetUserStats(User userDB);
        List<UserStat> GetUserStatByKey(User user, string key);
    }
}
