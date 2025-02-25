using ClassLibrary1.Models;
using DatabaseLayer;
using FitnessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Interfaces___Копировать
{
    public class UserRepository : IBaseRepository<User>
    {
        public UserContext UserContext { get; set; }

        public UserRepository(UserContext userContext)
        {
            UserContext = userContext;
        }



        public async Task CreateAsync(User entity)
        {
            await UserContext.Users.AddAsync(entity);
            await UserContext.SaveChangesAsync();
        }

        public IQueryable<User> GetAll()
        {
            return UserContext.Users;
        }

        public async Task DeleteAsync(User entity)
        {
            UserContext.Users.Remove(entity);
            await UserContext.SaveChangesAsync();
        }

        public async Task<User> UpdateAsync(User entity)
        {
            UserContext.Users.Update(entity);
            await UserContext.SaveChangesAsync();
            return entity;

        }
    }
}
