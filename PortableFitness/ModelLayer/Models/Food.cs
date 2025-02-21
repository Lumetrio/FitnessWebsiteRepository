using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClassLibrary1.Models
{
    // если быть вообще имбой можно ещё запихнуть хотите узнать как их готовить? 
    public class Food
    {
        // set у свойств нужен для json сериализации
        /// <summary>
        /// Белки
        /// </summary>
        [JsonPropertyName("Proteins")]
        public double Proteins { get; private set; }
        /// <summary>
        /// жиры
        /// </summary>
        [JsonPropertyName("Fats")]
        public double Fats { get; private set; }
        /// <summary>
        /// углеводы
        /// </summary>
        [JsonPropertyName("Carbohydrates")]
        public double Carbohydrates { get; private set; }

        /// <summary>
        /// Калории в 100 грамах
        /// </summary>
        [JsonPropertyName("Calories")]
        public double Calories { get; private set; }
        /// <summary>
        /// Наименование продукта
        /// </summary>
        [JsonPropertyName("Name")]
        public string Name { get; private set; }
        [JsonConstructor]

        public Food(
         string Name, double Proteins, double Fats, double Carbohydrates, double Calories
    )
        {
            this.Name = Name;
            this.Proteins = Proteins;
            this.Fats = Fats;
            this.Carbohydrates = Carbohydrates;
            this.Calories = Calories;
        }
        public Food(
    string name = "",
    double proteins = 0,
    double fats = 0,
    double carbohydrates = 0,
    int calories = 0
)
        {
            Name = name;
            Proteins = Math.Round(proteins / 100.0,4); // Переводим в граммы на 1 г продукта
            Fats = Math.Round(fats / 100.0,4);
            Carbohydrates = Math.Round(carbohydrates / 100.0,4);
            //почему-то калории обязательно нужно делить на 100.0 
            Calories =Math.Round( calories/100.0,4) ;
        }
    }
}
   