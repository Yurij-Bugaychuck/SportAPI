using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        Task<UserOption> UpdateUserOption(User userDB, UserOption option);
        Task<UserOption> RemoveUserOption(User userDB, Guid optionId);
        List<UserOption> GetUserOptions(User userDB);
        List<UserOption> GetUserOptionByKey(User user, string key);


        Task<UserStat> AddUserStat(User userDB, UserStat option);
        Task<UserStat> UpdateUserStat(User userDB, UserStat option);
        Task<UserStat> RemoveUserStat(User userDB, Guid statId);
        List<UserStat> GetUserStats(User userDB);
        List<UserStat> GetUserStatByKey(User user, string key);

        List<UserStat> GetUserStatByCategory(User user, Guid categoryId);


        Task<UserOption> NewAvatar(User user, IFormFile image);
        UserOption GetAvatar(User user);
        List<UserOption> GetAvatars(User user);

    }
}
