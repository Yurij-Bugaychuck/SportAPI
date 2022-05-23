using System;
using System.Collections.Generic;
using SportAPI.Models.User;

namespace SportAPI.Interfaces
{
    public interface ICategoriesService
    {
        void AddStatsCategory(StatsCategory cat);
        void UpdateStatsCategory(StatsCategory category);
        void RemoveStatsCategory(Guid categoryId);
        List<StatsCategory> GetStatsCategories();
        StatsCategory GetStatsCategoryById(Guid CatId);
    }
}
