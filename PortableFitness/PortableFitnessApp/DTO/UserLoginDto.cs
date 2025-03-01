using System.ComponentModel.DataAnnotations;

namespace PortableFitnessApp.DTO
{
    public class UserLoginDto
    {
      
            [Required(ErrorMessage = "Имя пользователя не может быть пустым")]
            public string Username { get; set; }

            [Required(ErrorMessage = "Пароль не может быть пустым")]
            public string Password { get; set; }

            //public bool RememberMe { get; set; } если будешь true то жить твой кукис будет дольше.
        
    }
}
