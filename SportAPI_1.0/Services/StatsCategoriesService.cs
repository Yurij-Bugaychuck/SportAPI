using System;
using System.Collections.Generic;
using System.Linq;
using SportAPI.Interfaces;
using SportAPI.Models;
using SportAPI.Models.User;

namespace SportAPI.Services
{
    public class StatsCategoriesService : ICategoriesService
    {
        private SportContext Context { get; }
        
        public StatsCategoriesService(SportContext dbContext)
        {
            this.Context = dbContext;
        }
        
        #region Public Methods

        public void AddStatsCategory(StatsCategory cat)
        {
            this.Context.StatsCategories.Add(cat);

            this.Context.SaveChanges();
        }
        
        public void UpdateStatsCategory(StatsCategory category)
        {
            this.Context.StatsCategories.Update(category);

            this.Context.SaveChanges();
        }
        
        public void RemoveStatsCategory(Guid categoryId)
        {
            var category = this.Context.StatsCategories
                .FirstOrDefault(statsCategory => statsCategory.StatsCategoryId == categoryId);

            if (category != null)
                this.Context.StatsCategories.Remove(category);

            this.Context.SaveChanges();
        }
        
        public List<StatsCategory> GetStatsCategories() => 
            this.Context.StatsCategories.ToList();
        
        public StatsCategory GetStatsCategoryById(Guid catId) => 
            this.Context.StatsCategories.FirstOrDefault(o => o.StatsCategoryId == catId);

        #endregion Public Methods
    }
}
