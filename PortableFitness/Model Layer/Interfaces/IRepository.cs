using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Interfaces___Копировать
{
    /// <summary>
    /// Интерфейс для команд баз данных.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T>
    {
        Task CreateAsync(T entity);
        IQueryable<T> GetAll();
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
    }
}
