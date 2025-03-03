using Database.Contexts;
using Database.Interfaces___Копировать;
using FitnessLogic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DBModelCommands
{
    /// <summary>
    ///  взаимодействие с репозиторием пользователей в DI со спец логикой на проверку уникальности пользователя.
    /// Пользователь регается через UserManager. нужно ли через него проверять пользователей? 
    /// </summary>

    public class UserRepository : RepositoryGen<User>
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }


        public async Task<bool> IsUsernameUniqueAsync(string username)
        {
            return !await _dbSet.AnyAsync(u => u.UserName == username);
        }
        public override async Task CreateAsync(User user)
        {

            if (!await IsUsernameUniqueAsync(user.UserName))
            {
                throw new Exception("User is not unique");
            }
            await _dbSet.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
