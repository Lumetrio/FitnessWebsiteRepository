using FitnessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Models
{
    //их сериализовать буду отдельно а не с пользователем.Может пользователю добавлю в файл список значений хорошо плохо если пожрал .
    public class NutritionNorms
    {
        //  У пользователя могут меняться данные. Роста веса соответственно калории тоже должны просчитываться.
        /// <summary>
        /// Калории необходимые пользователю в день
        /// </summary>
        public double CaloriesPerDay { get; set; }

        /// <summary>
        /// Белки в день
        /// </summary>
        public double ProteinsPerDay { get; set; }
        /// <summary>
        /// Жиры в день
        /// </summary>
        public double FatsPerDay { get; set; }
        /// <summary>
        /// Углеводы в день.
        /// </summary>
        public double CarbohydratesPerDay { get; set; }
        /// <summary>
        /// Пользователь для которого считаем полезные вещества.
        /// </summary>
        public User User { get; }
        /// <summary>
        /// рассчитает в зависимости от данных пользователя необходимые жиры углеводы калории и белки
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="ArgumentException"></exception>

        public NutritionNorms(User user)
        {
            double BMR;
            if (user.Gender == Gender.Male)
            {
                BMR = 88.362 + (13.397 * user.Weight) + (4.799 * user.Height) - (5.677 * user.Age);
            }
            else if (user.Gender == Gender.Female)
            {
                BMR = 447.593 + (9.247 * user.Weight) + (3.098 * user.Height) - (4.330 * user.Age);
            }
            else
            {
                throw new ArgumentException("Неизвестный пол пользователя.");
            }
            
            // Калории зависят от уровня активности
            switch (user.ActivityLevel)
            {
                case ActivityLevel.Low:
                    CaloriesPerDay = BMR * 1.2;
                    break;
                case ActivityLevel.Easy:
                    CaloriesPerDay = BMR * 1.375;
                    break;
                case ActivityLevel.Normal:
                    CaloriesPerDay = BMR * 1.55;
                    break;
                case ActivityLevel.High:
                    CaloriesPerDay = BMR * 1.725;
                    break;
            }
            

            // Учёт возраста и пола для распределения питательных веществ
            SetNutrientDistribution(user);
        }
        //private double AdjustForGoal(ref double BMR)
        //{
        //    switch (User.Goal)
        //    {

        //    }
        //}

        private void SetNutrientDistribution(User user)
        {
            // проверка исключения сразу. Хотя даты зарыты в пользователе. и там дата рождения не раньше 1903. Значит её нужно как-то обновлять из года в год. 
            if (user.Age < 5 || user.Age > 123)
            {
                throw new ArgumentOutOfRangeException("Возраст не может выходить за пределы поставленных для User значений");
            }
            // Расчёт белков, жиров и углеводов в зависимости от пола и возраста
            if (user.Gender == Gender.Male)
            {
                CalculateMaleNutrients(user);
            }
            else if (user.Gender == Gender.Female)
            {
                CalculateFemaleNutrients(user);
            }
        }

        // разбросы в % это бред делать так как из раза в раз могут быть совершенно разные значения.
        private void CalculateMaleNutrients(User user)
        {
            if (user.Age >= 5 && user.Age <=18) 
            {
                ProteinsPerDay = 2.0 * user.Weight; 
                FatsPerDay = 1.1 * user.Weight;     
            }
           else if (user.Age >= 19 && user.Age <= 30)
            {
                ProteinsPerDay = 1.8 * user.Weight; 
                FatsPerDay = 1.0 * user.Weight;    
            }
            else if (user.Age >= 31 && user.Age <= 50)
            {
                ProteinsPerDay = 1.7 * user.Weight; 
                FatsPerDay = 0.9 * user.Weight;     
            }
            else if (user.Age > 50)
            {
                ProteinsPerDay = 1.6 * user.Weight; 
                FatsPerDay = 0.8 * user.Weight;    
            }
        
            //углеводы как оставшиеся калории
            CarbohydratesPerDay = (CaloriesPerDay - (ProteinsPerDay * 4) - (FatsPerDay * 9)) / 4;
        }

        private void CalculateFemaleNutrients(User user)
        {
            if (user.Age >= 5 && user.Age < 19)
            {
                ProteinsPerDay = 1.9 * user.Weight;
                FatsPerDay = 1.0 * user.Weight;
            }
            else if (user.Age >= 19 && user.Age <= 30)
            {
                ProteinsPerDay = 1.7 * user.Weight;
                FatsPerDay = 0.9 * user.Weight;
            }
            else if (user.Age >= 31 && user.Age <= 50)
            {
                ProteinsPerDay = 1.6 * user.Weight;
                FatsPerDay = 0.8 * user.Weight;
            }
            else if (user.Age > 50) 
            {
                ProteinsPerDay = 1.5 * user.Weight;
                FatsPerDay = 0.7 * user.Weight;     
            }
          
            //углеводы как оставшиеся калории
            CarbohydratesPerDay = (CaloriesPerDay - (ProteinsPerDay * 4) - (FatsPerDay * 9)) / 4;
        }
        public NutritionNorms()
        {

        }
    }
}

/// <summary>
/// Уровень нагрузок
/// </summary>


