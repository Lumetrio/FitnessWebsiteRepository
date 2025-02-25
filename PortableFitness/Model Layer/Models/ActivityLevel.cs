using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Models
{
    /// <summary>
    /// Уровень активности в течении дня
    /// </summary>
    public enum ActivityLevel
    {

        /// <summary>
        /// Сидячий образ жизни: BMR × 1.2
        /// </summary>
        Low,
        /// <summary>
        ///   Легкие нагрузки: BMR × 1.375
        /// </summary>
        Easy,
        /// <summary>
        /// Умеренные нагрузки: BMR × 1.55
        /// </summary>
        Normal,
        /// <summary>
        ///   Высокая активность: BMR × 1.725
        /// </summary>
        High

    }
}
