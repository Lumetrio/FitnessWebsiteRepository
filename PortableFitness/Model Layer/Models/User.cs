using ClassLibrary1;
using ClassLibrary1.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FitnessLogic.Models
{

    // мб ему нужно автоматически считать сколько калорий надо?
    public class User
    {
        #region Набор свойств

        public int UserId { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        //[JsonPropertyName("name")]
        
        public string Name { get; private set; }

        public string Password { get; private set; }
        /// <summary>
        /// Пол
        /// </summary>
        //[JsonPropertyName("gender")]
        public Gender Gender { get; private set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        //[JsonPropertyName("birthDate")]
   
        public DateOnly BirthDate { get; private set; }

        /// <summary>
        /// Вес
        /// </summary>
        //[JsonPropertyName("weight")]
   
        public double Weight { get; set; }

        /// <summary>
        /// Рост
        /// </summary>
        //[JsonPropertyName("height")]
    
        public double Height { get; set; }

        /// <summary>
        /// Уровень активности
        /// </summary>
        //[JsonPropertyName("activityLevel")]
        public ActivityLevel ActivityLevel { get;  }
       
       

        /// <summary>
        /// Возраст (вычисляемое свойство)
        /// </summary>
        //[JsonIgnore]
        public int Age
        {
            get
            {
                var age = DateTime.Today.Year - BirthDate.Year;
                var date = DateTime.Today;
                DateOnly dateOnly = DateOnly.FromDateTime(date);
                if (dateOnly.AddYears(-age) < BirthDate)
                {
                    age--;
                }
                return age;
            }
        }

        #endregion

        /// <summary>
        /// Конструктор для десериализации
        /// </summary>
        //[JsonConstructor]
        public User(string name, string password, Gender gender, DateOnly  birthDate, double weight, double height, ActivityLevel activityLevel)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("Имя пользователя не может быть пустым", nameof(name)); // такое произойти не должно поэтому пока как доп мера безоп.
            Name = name;

            BirthDate = birthDate;

            Gender = gender;


            Weight = weight;
            Height = height;
            ActivityLevel = activityLevel;
            Password = password;
        }
        /// <summary>
        /// конструктор без параметров для бд
        /// </summary>
        public User()
        {
            
        }
        /// <summary>
        /// Конструктор для создания пользователей в коде
        /// </summary>
       
        // отношение 1 к многим
        public ICollection<Meal> Meals { get; set; }
        public override string ToString()
        {
            return $"{Name} ({Age} years old)";
        }
    }
}