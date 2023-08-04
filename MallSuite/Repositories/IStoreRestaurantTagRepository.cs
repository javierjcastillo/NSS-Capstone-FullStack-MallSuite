using System.Collections.Generic;
using MallSuite.Models;

namespace MallSuite.Repositories
{
    public interface IStoreRestaurantTagRepository
    {
        List<StoreRestaurantTag> GetAll();
    }
}
