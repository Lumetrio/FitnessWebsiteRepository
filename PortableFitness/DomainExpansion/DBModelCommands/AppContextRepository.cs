using Database.Interfaces___Копировать;
using Database.RepositoryObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DBModelCommands
{
    public class AppContextRepository : IBaseRepository<MealFood>
    {
        public Task CreateAsync(MealFood entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(MealFood entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<MealFood> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<MealFood> UpdateAsync(MealFood entity)
        {
            throw new NotImplementedException();
        }
    }
}
