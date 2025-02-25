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
        [JsonPropertyName("name")]
        [Required, MaxLength(30), MinLength(3)]
        public string Name { get; private set; }

        /// <summary>
        /// Пол
        /// </summary>
        [JsonPropertyName("gender")]
        public Gender Gender { get; private set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        [JsonPropertyName("birthDate")]
        public DateTime BirthDate { get; private set; }

        /// <summary>
        /// Вес
        /// </summary>
        [JsonPropertyName("weight")]
        public double Weight { get; set; }

        /// <summary>
        /// Рост
        /// </summary>
        [JsonPropertyName("height")]
        public double Height { get; set; }

        /// <summary>
        /// Уровень активности
        /// </summary>
        [JsonPropertyName("activityLevel")]
        public ActivityLevel ActivityLevel { get;  }
       
       

        /// <summary>
        /// Возраст (вычисляемое свойство)
        /// </summary>
        [JsonIgnore]
        public int Age
        {
            get
            {
                var age = DateTime.Today.Year - BirthDate.Year;
                var date = DateTime.Today;
                if (date.AddYears(-age) < BirthDate)
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
        [JsonConstructor]
        public User(string name, Gender gender, DateTime birthDate, double weight, double height, ActivityLevel activityLevel)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("Имя пользователя не может быть пустым", nameof(name));
            Name = name;
           
            BirthDate = birthDate;
            
            Gender = gender;
            if (birthDate < DateTime.Parse("01.01.1903") || birthDate >= DateTime.Parse("01.01.2020"))
            {
                throw new ArgumentException("Недопустимая дата рождения", nameof(birthDate));
            }

            if (weight <= 0)
            {
                throw new ArgumentException("Вес не может быть меньше или равен нулю", nameof(weight));
            }

            if (height <= 0)
            {
                throw new ArgumentException("Рост не может быть меньше или равен нулю", nameof(height));
            }

            Weight = weight;
            Height = height;
            ActivityLevel = activityLevel;
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
        public User(string name, GenderType genderType, DateTime birthDate, double weight, double height, ActivityLevel activityLevel)
            : this(name, genderType.Gender, birthDate, weight, height, activityLevel)
        {
            // Этот конструктор просто вызывает основной конструктор для десериализации
        }
        public ICollection<Meal> Meals { get; set; }
        public override string ToString()
        {
            return $"{Name} ({Age} years old)";
        }
    }
}