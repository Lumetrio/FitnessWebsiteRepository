using ClassLibrary1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.RepositoryObjects
{
    // так как Meal не может хранить в себе словарь food int а надо , так как иначе нельзя будет нормально соединять данные сделаем этот объект

    /// <summary>
    /// Доп сущность для работы бд. Хранит в себе вес. Её пользователь и будет вписывать а она попадать в Meal
    /// </summary>
    public class MealFood
    {
        public int MealId { get; set; }

        public Meal Meal { get; set; }

        public int FoodId { get; set; }

        public int Weight { get; set; }

        public Food Food { get; set; }

    }
}
