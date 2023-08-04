using System.Collections.Generic;
using MallSuite.Models;

namespace MallSuite.Repositories
{
    public interface IStoreRestaurantRepository
    {
        List<StoreRestaurant> GetAll();
        StoreRestaurant GetById(int id);
        void Add(StoreRestaurant storeRestaurant);
        void Edit(StoreRestaurant storeRestaurant);
        void Delete(int id);
        List<StoreRestaurant> GetByCategoryId(int categoryId);
        List<StoreRestaurant> GetByTagId(int tagId);
        void AddCategory(int storeRestaurantId, int categoryId);
        void UpdateCategory(int storeRestaurantId, int categoryId);
        void AddTag(int storeRestaurantId, int tagId);
        void RemoveTag(int storeRestaurantId, int tagId);
    }
}
