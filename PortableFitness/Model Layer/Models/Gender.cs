using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessLogic.Models
{
    /// <summary>
    /// Пол
    /// </summary>

    public class GenderType
    {
        //делается через бд поэтому в отдельный класс вместо перечисления ( у перечисления не может быть доп логики)
        //поэтому класс

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; }
        internal Gender Gender { get; }
        /// <summary>
        /// Создать новый пол
        /// </summary>
        /// <param name="name">Имя пола</param>
        /// <exception cref="ArgumentNullException">выкинется если пустое поле</exception>
        public GenderType(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            { 
                throw new ArgumentNullException("Имя пола не может быть пустым", nameof(name));
            }
            Name = name;

            switch (Name.ToLower())
            {
                case "муж":
                case "м": 
                case "мужик":            
                case "мужчина":
                case "m":
                case "male":
                case "мальчик":
                case "man":
                    Gender = Gender.Male;
                    break;
                default:
                    Gender = Gender.Female;break;
            }
        }
        public override string ToString()
        {
            return Name;
        }
    }
    public enum Gender
    {
        Male,
        Female
    }
}
