using Database.RepositoryObjects;
using FitnessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClassLibrary1.Models
{
    //по идее необходим id
    public class Meal
    {
        public int MealId { get; set; }

        public User User { get; set; } //навигация

        public ICollection<MealFood> MealFoods { get; set; }
        public DateTime MealDate { get; set; } = DateTime.Now;

        public string UserId { get; set; }

        public Meal()
        {

        }
    }
}
