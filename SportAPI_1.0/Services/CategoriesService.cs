using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using SportAPI.Interfaces;
using SportAPI.Models;
using System.Security.Authentication;

namespace SportAPI.Services
{
    public class CategoriesService : ICategoriesService
    {
        SportContext _context;
        public CategoriesService(SportContext dbContext)
        {
            _context = dbContext;
        }

        public void AddStatsCategory(StatsCategory cat) {
            _context.StatsCategories.Add(cat);

            _context.SaveChangesAsync();
        }
        public void UpdateStatsCategory(StatsCategory cat)
        {
            _context.StatsCategories.Update(cat);

            _context.SaveChanges();
        }
        public void RemoveStatsCategory(Guid catId)
        {
            var cat = _context.StatsCategories.FirstOrDefault(o => o.StatsCategoryId == catId);
            _context.StatsCategories.Remove(cat);
            _context.SaveChanges();
        }
        public List<StatsCategory> GetStatsCategories()
        {
            var categories = _context.StatsCategories.ToList();

            return categories;
        }
        public StatsCategory GetStatsCategoryById(Guid catId)
        {
            var cat = _context.StatsCategories.FirstOrDefault(o => o.StatsCategoryId == catId);

            return cat;
        }




    }
}
