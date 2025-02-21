using ClassLibrary1.Controllers.Interfaces;
using ClassLibrary1.Models.ConsoleRealization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models.ConsoleRealization.AppMenu
{
    /// <summary>
    /// Меню для упрощения работы пользователю. В саб меню добавил паттерн команда. 
    /// </summary>
    
    // Если понадобится то может стоить добавить команду и для 1 главного меню.
    public class Menu
    {
        /// <summary>
        /// Реализация паттерна команда с помощью словаря.
        /// </summary>
       public Dictionary<int, ICommand> Commands=new Dictionary<int, ICommand>();

        /// <summary>
        /// Реализация паттерна команда. Добавление в словарь команд команду и номер на меню.
        /// </summary>
        
        //можно подумать как проверять чтобы случайно не добавил не в тот номер команду. К примеру добавлять имя. и сравнивать его с submenu опциями
        public void AddCommand(int id, ICommand command)
        {
            Commands.Add(id, command);
        }
        /// <summary>
        /// Позиция меню в ширине по экрану консоли.
        /// </summary>
        private const int MenuXPosition = 40;
        /// <summary>
        /// Позиция меню на высоте по экрану консоли.
        /// </summary>
        private const int MenuYStart = 12;

        /// <summary>
        /// символ индекса
        /// </summary>
        const char _symbolIndex = '<';
        /// <summary>
        /// Опции
        /// </summary>
        string[] _mainMenuOptions = new string[] { "Старт", "Справка", "Выход" };
        /// <summary>
        /// Опции после нажатия старт
        /// </summary>
       // мб рост вес может меняться ток раз за день? Или в пределах разумного.
        string[] _subMenuStartOptions = new string[] { "Посчитать калории в еде", "Посмотреть сколько ещё нужно съесть ", "Посмотреть что ел за...","Что съесть", "Поменять вес/рост пользователя" };
        /// <summary>
        /// Курсор находиться здесь
        /// </summary>
        private int _selectedIndex;
        /// <summary>
        /// Состояние меню.
        /// </summary>
        private MenuState _currentState;
       
        public Menu()
        {
            _selectedIndex = 0;
            _currentState = MenuState.Main;
        }
        /// <summary>
        /// Вырисовка меню.
        /// </summary>

        private void DrawMenu(string[] options)
        {
            Console.SetCursorPosition(MenuXPosition, MenuYStart);
            for (int i = 0; i < options.Length; i++)
            {
                string prefix;
                if (i == _selectedIndex)
                {
                    prefix = _symbolIndex.ToString();
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    prefix = " ";
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.SetCursorPosition(MenuXPosition, MenuYStart + 1 + i);
                Console.Write($"{prefix} {options[i]}");

            }
        }
        /// <summary>
        /// Обработка навигации в меню
        /// </summary>
        public void Run()
        {
            Console.Clear();
            ConsoleKey pressedKey;
            string[] currentOptions = _currentState == MenuState.Main
                      ? _mainMenuOptions
                      : _subMenuStartOptions;
            do
            {
                DrawMenu(currentOptions);
                pressedKey = Console.ReadKey(true).Key;

                if (pressedKey == ConsoleKey.W||pressedKey==ConsoleKey.UpArrow)
                {
                    _selectedIndex = (_selectedIndex - 1 + currentOptions.Length) % currentOptions.Length;
                }
                if (pressedKey == ConsoleKey.S||pressedKey == ConsoleKey.DownArrow)
                {
                    _selectedIndex = (_selectedIndex + 1) % currentOptions.Length;
                }
            } while (pressedKey != ConsoleKey.Enter);
            Console.BackgroundColor = ConsoleColor.Black; Console.ForegroundColor = ConsoleColor.White;
            ProcessSelectedOption(currentOptions);
            Console.Clear();
        }

        /// <summary>
        /// вкладка справка
        /// </summary>
        private void ShowHelp()
        {
            Console.Clear();
            Console.WriteLine("==============================================================================");
            Console.WriteLine("|                              СПРАВКА ПРИЛОЖЕНИЯ                             |");
            Console.WriteLine("|                          \"Подсчет калорий\"                                 |");
            Console.WriteLine("==============================================================================");

            Console.WriteLine("\nДобро пожаловать в приложение для подсчета калорий! Это простой инструмент,");
            Console.WriteLine("помогающий вам отслеживать потребление калорий, белков, жиров и углеводов.\n");

            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine("|                                ОСНОВНЫЕ КОМАНДЫ                              |");
            Console.WriteLine("------------------------------------------------------------------------------");

            Console.WriteLine("  - W: Перемещение вверх");
            Console.WriteLine("  - S: Перемещение вниз");
            Console.WriteLine("  - A: (Дополнительные функции, если применимо)");
            Console.WriteLine("  - D: (Дополнительные функции, если применимо)");
            Console.WriteLine("  - Enter: Подтверждение выбора");
            Console.WriteLine("  - Escape: Возврат в предыдущее меню или выход из справки\n");

            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine("|                               ФУНКЦИОНАЛ                                    |");
            Console.WriteLine("------------------------------------------------------------------------------");

            Console.WriteLine("1. [Старт]");
            Console.WriteLine("   - Начать работу с приложением.");
            Console.WriteLine("   - Выберите пользователя или создайте нового.");
            Console.WriteLine("   - Добавляйте продукты и отслеживайте приемы пищи.\n");

            Console.WriteLine("2. [Справка]");
            Console.WriteLine("   - Отображение данной справки.");
            Console.WriteLine("   - Информация о командах и функционале приложения.\n");

            Console.WriteLine("3. [Выход]");
            Console.WriteLine("   - Завершение работы с приложением.\n");

            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine("|                           ДОПОЛНИТЕЛЬНАЯ ИНФОРМАЦИЯ                         |");
            Console.WriteLine("------------------------------------------------------------------------------");

            Console.WriteLine("- При добавлении продуктов:");
            Console.WriteLine("  * Укажите название продукта.");
            Console.WriteLine("  * Введите вес порции.");
            Console.WriteLine("  * Приложение автоматически рассчитает количество калорий, белков, жиров и ");
            Console.WriteLine("    углеводов на основе имеющейся базы данных.\n");

            Console.WriteLine("- Если продукт отсутствует в базе данных:");
            Console.WriteLine("  * Вы сможете добавить его самостоятельно.");
            Console.WriteLine("  * Укажите значения белков, жиров, углеводов и калорий на 100 грамм продукта.\n");

            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine("|                            ПОЛЕЗНЫЕ СОВЕТЫ                                  |");
            Console.WriteLine("------------------------------------------------------------------------------");

            Console.WriteLine("- Регулярно используйте приложение для отслеживания вашего рациона.");
            Console.WriteLine("- Корректируйте данные о продуктах, если они не соответствуют вашим источникам.");
            Console.WriteLine("- Не забывайте сохранять прогресс, чтобы вернуться к нему позже.\n");

            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine("|                           КОНТАКТЫ И ПОДДЕРЖКА                              |");
            Console.WriteLine("------------------------------------------------------------------------------");

            Console.WriteLine("Если у вас возникли вопросы или проблемы, свяжитесь с нами:");


            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine("|                              БЛАГОДАРНОСТЬ                                 |");
            Console.WriteLine("------------------------------------------------------------------------------");

            Console.WriteLine("Спасибо, что выбрали наше приложение! Мы надеемся, что оно поможет вам достичь");
            Console.WriteLine("ваших целей в здоровом питании и поддержании формы.\n");

            Console.WriteLine("==============================================================================");
            Console.WriteLine("|                        Нажмите ESC для выхода                               |");
            Console.WriteLine("==============================================================================");
            while (Console.ReadKey(true).Key != ConsoleKey.Escape) { }
            //Console.Clear();
            Run();
        }
        /// <summary>
        /// Обрабатывает выбранный пункт.
        /// </summary>
        /// <param name="options"></param>
        private void ProcessSelectedOption(string[] options)
        {
            Console.Clear();
            if (_currentState == MenuState.SubMenu)
            {
                // мб придётся создавать паттерн команду.
                switch (_selectedIndex)
                {
                    case 0:
                       
                        Commands[_selectedIndex]?.Execute();
                        break;
                    case 1:
                    
                        Commands[_selectedIndex].Execute();
                        while (Console.ReadKey(true).Key != ConsoleKey.Escape) { }
                        Run();
                        break;
                    case 2:
                       
                        Commands[_selectedIndex]?.Execute();
                        //while (Console.ReadKey(true).Key != ConsoleKey.Escape) { }
                        //Run();
                        break;
                    case 3:
                        //даёт рекомендацию по еде

                        Commands[_selectedIndex]?.Execute();
                        //Run();
                        break;
                    case 4:
                        //меняет вес рост пользователя
                        Commands[_selectedIndex]?.Execute();
                        break;
                }
            }
            if (_currentState == MenuState.Main)
            {
                switch (_selectedIndex)
                {
                    case 0:

                        _currentState = MenuState.SubMenu;
                        _selectedIndex = 0;
                        //Run();
                        break;
                    case 1: // Справка
                        ShowHelp();
                        break;
                    case 2: // Выход
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}

