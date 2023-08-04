using System.Collections.Generic;
using MallSuite.Models;

namespace MallSuite.Repositories
{
    public interface ITagRepository
    {
        List<Tag> GetAll();
        Tag GetById(int id);
        void Add(Tag tag);
        void Delete(int tagId);
        void Edit(Tag tag);
        //void AddTagToStoreRestaurant(int storeRestaurantId, int tagId);
        //void RemoveTagFromStoreRestaurant(int storeRestaurantId, int tagId);
    }
}
