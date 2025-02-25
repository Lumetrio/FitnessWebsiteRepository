using ClassLibrary1.Models;
using Database.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DBModelCommands
{
    public class MealRepository : RepositoryGen<Meal>
    {
        public MealRepository(AppDbContext context) : base(context)
        {
        }
    }
}
