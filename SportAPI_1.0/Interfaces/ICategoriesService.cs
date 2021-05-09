using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportAPI.Models;
namespace SportAPI.Interfaces
{
    public interface ICategoriesService
    {
        void AddStatsCategory(StatsCategory cat);
        void UpdateStatsCategory(StatsCategory cat);
        void RemoveStatsCategory(Guid cat);
        List<StatsCategory> GetStatsCategories();
        StatsCategory GetStatsCategoryById(Guid CatId);
    }
}
