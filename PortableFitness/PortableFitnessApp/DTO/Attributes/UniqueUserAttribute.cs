using Database.DBModelCommands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PortableFitnessApp.DTO.Attributes
{
    // получается какая-то дичь. Модели не должны зависеть ни от чего, так что это походу нельзя вставить как атрибут валидации...
    public class UniqueUserNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var userRepository = (UserRepository)validationContext.GetService(typeof(UserRepository))??throw new ArgumentNullException("UserRepository отсутствует в DI");
            var username = value as string;
            if (string.IsNullOrEmpty(username))
            {
                return ValidationResult.Success;
            }

            if (!userRepository.IsUsernameUniqueAsync(username).Result)
            {
                return new ValidationResult("Имя пользователя уже занято.");
            }

            return ValidationResult.Success;
        }
    }
}
