using System.Collections.Generic;
using MallSuite.Models;

namespace MallSuite.Repositories
{
    public interface IUserRepository
    {
        User GetByFirebaseUserId(string firebaseUserId);
        User GetById(int id);
        void Add(User user);
        void Delete(int userId);
    }
}
