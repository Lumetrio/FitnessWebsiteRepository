using System.ComponentModel.DataAnnotations;
using ClassLibrary1.Models;
using FitnessLogic.Models;
using PortableFitnessApp.DTO.Attributes;

namespace PortableFitnessApp.DTO
{
        public class UserRegisterDto
        {
            [UniqueUserName]
            [Required(ErrorMessage = "Имя пользователя не может быть пустым")]
            [MaxLength(30, ErrorMessage = "Имя пользователя не может быть длиннее 30 символов")]
            [MinLength(3, ErrorMessage = "Имя пользователя не может быть короче 3 символов")]
            public string Name { get; set; }
            [Required, MaxLength(55), MinLength(8)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d).+$", ErrorMessage = "Пароль слишком прост")] // пароль содержит 1 букву И 1 цифру мин
        public string Password { get; set; }

            [Required(ErrorMessage = "Пол обязателен для выбора.")]
            public Gender Gender { get; set; }

            [Range(typeof(DateOnly), "01.01.1903", "01.01.2020", ErrorMessage = "Недопустимая дата рождения")]
            public DateOnly BirthDate { get; set; }

            //[Required(ErrorMessage = "Вес обязателен для заполнения.")]
            [Range(4.0, 500, ErrorMessage = "Вес ошибочен")]
            public double Weight { get; set; }

            [Required(ErrorMessage = "Рост обязателен для заполнения.")]
            [Range(11.0, 500, ErrorMessage = "Рост ошибочен")]
            public double Height { get; set; }

            
            [Range(0, 3, ErrorMessage = "Уровень активности должен быть от 0 до 3.")]
            public int ActivityLevel { get; set; }

        public static explicit operator User(UserRegisterDto userDto)
        {
            if (userDto == null)
            {
                throw new ArgumentNullException(nameof(userDto), "DTO не может быть null.");
            }
            var user = new User(userDto.Name, userDto.Password, userDto.Gender, userDto.BirthDate, userDto.Weight, userDto.Height, (ActivityLevel)userDto.ActivityLevel);
            // вопрос нужно ли для пользователя связать напрямую nutritionnorms

            return user;
        }
    }
}
