using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportAPI.Models;
namespace SportAPI.Interfaces
{
    interface ICategoriesService
    {
        void AddStatsCategory(StatsCategories cat);
        void UpdateStatsCategory(StatsCategories cat);
        void RemoveStatsCategory(StatsCategories cat);
        void GetStatsCategories(StatsCategories cat);
        void GetStatsCategoryById(StatsCategories cat);
    }
}
