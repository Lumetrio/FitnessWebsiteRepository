using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClassLibrary1.Models
{
    /// <summary>
    /// Съеденная еда. Нужна для десериализации из файлов записанных meal/
    /// </summary>
    /// копирует MEAL
    public class DevouredFood
    {
        [JsonPropertyName("food")]
        public Food Food { get; set; }

        [JsonPropertyName("weight")]
        public double Weight { get; set; }

        [JsonPropertyName("ateAtDate")]
        public DateTime MealDate { get; set; }=DateTime.Now;
    }
}
