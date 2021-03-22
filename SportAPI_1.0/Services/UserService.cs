using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using SportAPI.Interfaces;
using SportAPI.Models;
namespace SportAPI.Services
{
    public class UserService : IUserService, ISecurityService
    {
        SportContext _context;
        public UserService(SportContext dbContext)
        {
            _context = dbContext;
        }

        public bool CanRead(User user)
        {
            return true;
        }

        public User GetByEmail(string email)
        {
            return _context.Users.Include(o => o.Workouts).FirstOrDefault(o => o.Email == email);
        }
        
        public User GetById(Guid id)
        {
            return _context.Users.FirstOrDefault(o => o.UserId == id);
        }

        public async Task<Guid> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user.UserId;
        }

        public async Task<User> UpdateUser(User userDB, User user)
        {
            var props = user.GetType().GetProperties();
            foreach (var prop in props)
            {
                var tmp = prop.GetValue(user, null);
                prop.SetValue(userDB, tmp);
            }

            _context.Entry(userDB).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Modified;
            await _context.SaveChangesAsync();

            return userDB;
        }

        //------------
        //User Options
        //------------

        public async Task<UserOption> AddUserOption(User user, UserOption option)
        {
            option.UserId = user.UserId;

            var userOption = _context.UsersOptions.Add(option);
            await _context.SaveChangesAsync();

            return _context.UsersOptions.FirstOrDefault(o=>o.UserOptionsId == option.UserOptionsId);
        }


        public Dictionary<string, string> GetUserOptions(User user)
        {
            var userOptions = (from o in _context.UsersOptions
                               where o.UserId == user.UserId
                               orderby o.CreatedAt descending
                               select (new { key = o.Key, value = o.Value, Created_at = o.CreatedAt }))
                           .AsEnumerable().GroupBy(o => o.key).ToDictionary(key => key.Key, value => value.Select(o => o.value).FirstOrDefault());

            return userOptions;
        }
        
        public List<UserOption> GetUserOptionByKey(User user, string key)
        {
            var userOptions = _context.UsersOptions.Where(o => o.UserId == user.UserId && o.Key == key).OrderBy(o => o.CreatedAt).ToList();

            return userOptions;
        }

        //----------
        //User Stats
        //----------
        public async Task<UserStat> AddUserStat(User user, UserStat stat)
        {
            stat.UserId = user.UserId;

            var userOption = _context.UsersStats.Add(stat);
            await _context.SaveChangesAsync();

            return _context.UsersStats.FirstOrDefault(o => o.UserStatsId == stat.UserStatsId);
        }


        public Dictionary<string, int> GetUserStats(User user)
        {
            var usersStats = (from o in _context.UsersStats
                               where o.UserId == user.UserId
                               orderby o.CreatedAt descending
                               select (new { key = o.Key, value = o.Value, Created_at = o.CreatedAt }))
                           .AsEnumerable().GroupBy(o => o.key).ToDictionary(key => key.Key, value => value.Select(o => o.value).FirstOrDefault());

            return usersStats;
        }

        public List<UserStat> GetUserStatByKey(User user, string key)
        {
            var userStats = _context.UsersStats.Where(o => o.UserId == user.UserId && o.Key == key).OrderBy(o => o.CreatedAt).ToList();

            return userStats;
        }


    }
}
