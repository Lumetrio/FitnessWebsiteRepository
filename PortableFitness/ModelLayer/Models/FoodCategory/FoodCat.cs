using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClassLibrary1.Models.FoodCategory
{
    // буду создавать 1 файл с рекомендациями для еды.
    public class FoodCategory
    {
        // мб сделать категорию enum
        [JsonPropertyName("Category")]
        public  Category Category { get; }

        [JsonPropertyName("Foods")]
        public List<Food> Foods { get; }

        [JsonConstructor]
        public FoodCategory(Category category, List<Food> foods)
        {
            Category = category;
            Foods = foods;
        }
        // мб ещё сюда добавить string Recipe
    }
}
