using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Model_Layer.Models.Attributes
{
    public class UniqueUserNameAttribute : ValidationAttribute

    {
        public UserRepository UserRepository  { get; set; }
    public UniqueUserNameAttribute()
        {
            
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var username = value as string;
            if (string.IsNullOrEmpty(username))
            {
                return ValidationResult.Success;
            }

           

            if (userRepository.IsUsernameUniqueAsync(username).Result == false)
            {
                return new ValidationResult("Имя пользователя уже занято.");
            }

            return ValidationResult.Success;
        }
    }
}
