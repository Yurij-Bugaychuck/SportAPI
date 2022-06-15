using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportAPI.Interfaces;
using SportAPI.Models;
using System.Security.Authentication;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using SportAPI.Models.User;

namespace SportAPI.Services
{
    public class UserService : IUserService
    {
        private SportContext Context { get; }
        private ImageService ImageService { get; }
        private IWebHostEnvironment AppEnvironment { get; }

        public UserService(SportContext dbContext, ImageService imageService, IWebHostEnvironment appEnvironment)
        {
            this.Context = dbContext;
            this.ImageService = imageService;
            this.AppEnvironment = appEnvironment;
        }

        public User GetByEmail(string email)
        {
            return this.Context.Users
                .FirstOrDefault(o => o.Email == email);
        }

        public User GetById(Guid id)
        {
            return this.Context.Users.FirstOrDefault(o => o.UserId == id);
        }

        public async Task<Guid> CreateUser(User user)
        {
            this.Context.Users.Add(user);
            await this.Context.SaveChangesAsync();

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

            this.Context.Users.Update(userDB);
            await this.Context.SaveChangesAsync();

            return userDB;
        }

        //------------
        //User Options
        //------------

        public async Task<UserOption> AddUserOption(User user, UserOption option)
        {
            option.UserId = user.UserId;

            var userOption = this.Context.UsersOptions.Add(option);
            await this.Context.SaveChangesAsync();

            return this.Context.UsersOptions.FirstOrDefault(o => o.UserOptionsId == option.UserOptionsId);
        }

        public async Task<UserOption> UpdateUserOption(User user, UserOption option)
        {
            var optionDB = this.Context.UsersOptions.FirstOrDefault(o => o.UserOptionsId == option.UserOptionsId);

            if (optionDB.UserId != user.UserId)
                throw new AuthenticationException();

            if (option.Key != null)
                optionDB.Key = option.Key;

            if (option.Value != null)
                optionDB.Value = option.Value;

            this.Context.UsersOptions.Update(optionDB);
            await this.Context.SaveChangesAsync();

            return this.Context.UsersOptions.FirstOrDefault(o => o.UserOptionsId == optionDB.UserOptionsId);
        }

        public async Task<UserOption> RemoveUserOption(User user, Guid optionId)
        {
            var option = this.Context.UsersOptions.FirstOrDefault(o => o.UserOptionsId == optionId);

            if (option.UserId != user.UserId)
                throw new AuthenticationException();

            this.Context.UsersOptions.Remove(option);
            await this.Context.SaveChangesAsync();

            return option;
        }

        public List<UserOption> GetUserOptions(User user)
        {
            var userOptions = this.Context.UsersOptions
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
            var userOptions = this.Context.UsersOptions.Where(o => o.UserId == user.UserId && o.Key == key)
                .OrderBy(o => o.CreatedAt)
                .ToList();

            return userOptions;
        }

        //----------
        //User Stats
        //----------
        public async Task<UserStat> AddUserStat(User user, UserStat stat)
        {
            stat.UserId = user.UserId;

            this.Context.UsersStats.Add(stat);
            await this.Context.SaveChangesAsync();

            return this.Context.UsersStats.FirstOrDefault(o => o.UserStatsId == stat.UserStatsId);
        }

        public List<UserStat> GetUserStats(User user)
        {
            var userStats = this.Context.UsersStats
                .Where(o => o.UserId == user.UserId)
                .OrderBy(o => o.CreatedAt)
                .ToList();

            return userStats;
        }

        public List<UserStat> GetUserStatByKey(User user, string key)
        {
            var userStats = this.Context.UsersStats
                .Where(o => o.UserId == user.UserId && o.Key == key)
                .OrderBy(o => o.CreatedAt)
                .ToList();

            return userStats;
        }

        public List<UserStat> GetUserStatByCategory(User user, Guid categoryId)
        {
            var userStats = this.Context.UsersStats
                .Where(o => o.UserId == user.UserId && o.StatsCategoryId == categoryId)
                .OrderBy(o => o.CreatedAt)
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

        public async Task<UserStat> UpdateUserStat(User user, UserStat stat)
        {
            var statDB = this.Context.UsersStats
                .FirstOrDefault(o => stat.UserStatsId == o.UserStatsId);

            if (statDB.UserId != user.UserId)
                throw new AuthenticationException();

            if (stat.Key != null)
                statDB.Key = stat.Key;

            if (stat.Value != null)
                statDB.Value = stat.Value;

            if (stat.StatsCategoryId != null)
                statDB.StatsCategoryId = stat.StatsCategoryId;

            this.Context.UsersStats.Update(statDB);
            await this.Context.SaveChangesAsync();

            return this.Context.UsersStats.FirstOrDefault(o => o.UserStatsId == stat.UserStatsId);
        }

        public async Task<UserStat> RemoveUserStat(User user, Guid statId)
        {
            var stat = this.Context.UsersStats.FirstOrDefault(o => o.UserStatsId == statId);

            if (stat.UserId != user.UserId)
                throw new AuthenticationException();

            this.Context.UsersStats.Remove(stat);
            await this.Context.SaveChangesAsync();

            return stat;
        }

        public async Task<UserOption> NewAvatar(User user, IFormFile image)
        {
            var avatarsCount = this.GetAvatars(user).Count;

            string StartPath = Path.Combine(
                this.AppEnvironment.WebRootPath,
                "UsersAvatar",
                user.UserId.ToString());

            string ImgPath = await ImageService.NewImage(StartPath, image, avatarsCount);

            string FileExt = Path.GetExtension(image.FileName).ToLower();

            string resUrl = "UsersAvatar/" + user.UserId.ToString() + "/avatar-" + avatarsCount.ToString() + FileExt;

            var ImgOption = new UserOption {Key = "avatar", Value = resUrl};

            var res = await this.AddUserOption(user, ImgOption);

            return ImgOption;
        }

        public UserOption GetAvatar(User user)
        {
            var avatar = this.GetUserOptionByKey(user, "avatar").OrderByDescending(o => o.CreatedAt).FirstOrDefault();

            if (avatar == null)
            {
                string defaultPath = "UsersAvatar/default.png";

                avatar = new UserOption
                {
                    UserId = user.UserId,
                    Key = "avatar",
                    Value = defaultPath
                };
            }

            return avatar;
        }

        public List<UserOption> GetAvatars(User user)
        {
            var avatars = this.GetUserOptionByKey(user, "avatar");

            return avatars;
        }

        public UserOption GetAbout(User user)
        {
            var about = this.GetUserOptionByKey(user, "about").FirstOrDefault();

            if (about == null)
            {
                string defaultValue = "...";

                about = new UserOption
                {
                    UserId = user.UserId,
                    Key = "about",
                    Value = defaultValue
                };

                this.Context.UsersOptions.Add(about);
                this.Context.SaveChanges();
            }

            return about;
        }

        public UserOption UpdateAbout(User user, string aboutValue)
        {
            var about = this.GetAbout(user);

            about.Value = aboutValue;

            this.Context.UsersOptions.Update(about);
            this.Context.SaveChanges();

            return about;
        }
    }
}