using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using SportAPI.Interfaces;
using SportAPI.Models;
using System.Security.Authentication;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Routing;
namespace SportAPI.Services
{
    public class UserService : IUserService, ISecurityService
    {
        SportContext _context;
        ImageService _imageService;
        IWebHostEnvironment _appEnvironment;
        public UserService(SportContext dbContext, ImageService imageService, IWebHostEnvironment appEnvironment)
        {
            _context = dbContext;
            _imageService = imageService;
            _appEnvironment = appEnvironment;
        }

        public bool CanRead<UserOption>(User user, UserOption userOption)
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

            if (user.FirstName != null)
                userDB.FirstName = user.FirstName;
            if (user.LastName != null)
                userDB.LastName = user.LastName;
            if (user.Phone != null)
                userDB.Phone = user.Phone;

            _context.Users.Update(userDB);
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

            return _context.UsersOptions.FirstOrDefault(o => o.UserOptionsId == option.UserOptionsId);
        }

        public async Task<UserOption> UpdateUserOption(User user, UserOption option)
        {
            var optionDB = _context.UsersOptions.FirstOrDefault(o => o.UserOptionsId == option.UserOptionsId);
            
            if (optionDB.UserId != user.UserId) throw new AuthenticationException();

            if (option.Key != null)
                optionDB.Key = option.Key;
            if (option.Value != null)
                optionDB.Value = option.Value;

            _context.UsersOptions.Update(optionDB);
            await _context.SaveChangesAsync();

            return _context.UsersOptions.FirstOrDefault(o => o.UserOptionsId == optionDB.UserOptionsId);
        }

        public async Task<UserOption> RemoveUserOption(User user, Guid optionId)
        {
            var option = _context.UsersOptions.FirstOrDefault(o => o.UserOptionsId == optionId);
            
            if (option.UserId != user.UserId) throw new AuthenticationException();

            _context.UsersOptions.Remove(option);
            await _context.SaveChangesAsync();

            return option;
        }


        public List<UserOption> GetUserOptions(User user)
        {
            var userOptions = _context.UsersOptions
                .Where(o => o.UserId == user.UserId)
                .OrderByDescending(o => o.CreatedAt)
                .AsEnumerable()
                .GroupBy(o => o.Key)
                .ToDictionary(o => o.Key);

            var res = new List<UserOption>();
            foreach (var i in userOptions)
            {
                res.Add(i.Value.FirstOrDefault());
            }

            return res;
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

            _context.UsersStats.Add(stat);
            await _context.SaveChangesAsync();

            return _context.UsersStats.FirstOrDefault(o => o.UserStatsId == stat.UserStatsId);
        }


        public List<UserStat> GetUserStats(User user)
        {
            var userStats = _context.UsersStats
                .Where(o => o.UserId == user.UserId)
                .OrderByDescending(o => o.CreatedAt)
                .AsEnumerable()
                .GroupBy(o => o.Key) 
                .ToDictionary(o => o.Key);

            var res = new List<UserStat>();
            foreach (var i in userStats)
            {
                res.Add(i.Value.FirstOrDefault());
            }

            return res;
        }

        public List<UserStat> GetUserStatByKey(User user, string key)
        {
            var userStats = _context.UsersStats
                .Where(o => o.UserId == user.UserId && o.Key == key)
                .OrderByDescending(o => o.CreatedAt)
                .ToList();
            
            return userStats;
        }

        public List<UserStat> GetUserStatByCategory(User user, Guid categoryId)
        {
            var userStats = _context.UsersStats
                .Where(o => o.UserId == user.UserId && o.StatsCategoryId == categoryId)
                .OrderBy(o => o.CreatedAt).OrderByDescending(o => o.CreatedAt)
                .AsEnumerable()
                .GroupBy(o => o.Key)
                .ToDictionary(o => o.Key);

            var res = new List<UserStat>();
            foreach (var i in userStats)
            {
                res.Add(i.Value.FirstOrDefault());
            }

            return res;
        }


        public async Task<UserStat> UpdateUserStat(User user, UserStat stat)
        {
            var statDB = _context.UsersStats
                .FirstOrDefault(o => stat.UserStatsId == o.UserStatsId);
            
            if (statDB.UserId != user.UserId) throw new AuthenticationException();

            if (stat.Key != null)
                statDB.Key = stat.Key;
            if (stat.Value != null)
                statDB.Value = stat.Value;
            if (stat.StatsCategoryId != null)
                statDB.StatsCategoryId = stat.StatsCategoryId;

            _context.UsersStats.Update(statDB);
            await _context.SaveChangesAsync();

            return _context.UsersStats.FirstOrDefault(o => o.UserStatsId == stat.UserStatsId);
        }

        public async Task<UserStat> RemoveUserStat(User user, Guid statId)
        {
            var stat = _context.UsersStats.FirstOrDefault(o => o.UserStatsId == statId);

            if (stat.UserId != user.UserId) throw new AuthenticationException();

            _context.UsersStats.Remove(stat);
            await _context.SaveChangesAsync();

            return stat;
        }

        public async Task<UserOption> NewAvatar(User user, IFormFile image)
        {
            var avatarsCount = GetAvatars(user).Count;

            string StartPath = Path.Combine(
                _appEnvironment.WebRootPath,
                "UsersAvatar",
                user.UserId.ToString());


            string ImgPath = await _imageService.newImage(StartPath, image, avatarsCount);

            string FileExt = System.IO.Path.GetExtension(image.FileName).ToLower();

            string resUrl = "UsersAvatar/"+ user.UserId.ToString() + "/avatar-" + avatarsCount.ToString() + FileExt;

            var ImgOption = new UserOption { Key = "avatar", Value = resUrl };

            var res = await AddUserOption(user, ImgOption);

            return ImgOption;
        }
        public UserOption GetAvatar(User user)
        {
            var avatar = GetUserOptionByKey(user, "avatar").OrderByDescending(o => o.CreatedAt).FirstOrDefault();

            if (avatar == null)
            {
                string defaultPath = "UsersAvatar/default.png";
                avatar = new UserOption
                {
                    Key = "avatar",
                    Value = defaultPath
                };
            }
            return avatar;
        }
        public List<UserOption> GetAvatars(User user)
        {
            var avatars = GetUserOptionByKey(user, "avatar");

            return avatars;
        }


    }
}
