using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportAPI.Models;
namespace SportAPI.Interfaces
{
    public interface ICategoriesService
    {
        void AddStatsCategory(StatsCategories cat);
        void UpdateStatsCategory(StatsCategories cat);
        void RemoveStatsCategory(Guid cat);
        List<StatsCategories> GetStatsCategories();
        StatsCategories GetStatsCategoryById(Guid CatId);
    }
}
