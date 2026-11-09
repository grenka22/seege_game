//главное меню и возвращение выбора пользователя
using System;
namespace Seege.Presentation
{
    /// <summary>
    /// Действие, которое пользователь выбрал в главном меню.
    /// </summary>
    public enum MainMenuAction
    {
        NewGame,
        LoadGame,
        Records,
        Rules,
        Exit
    }

    /// <summary>
    /// Показывает главное меню и возвращает выбранный пункт.
    /// </summary>
    public class MainMenu
    {
        private const int ConsoleWidth = 80;

        /// <summary>
        /// Выводит меню и повторяет запрос, пока не будет введён номер пункта от 1 до 5.
        /// </summary>
        /// <returns>Выбранное действие меню.</returns>
        public MainMenuAction ShowMainMenu()
        {
            while (true) // бесконечный цикл пока не выберут решение
            {
                Console.Clear();
                Console.WriteLine("                ");
                Console.WriteLine(" SEEGE GAME 5x5 ");
                Console.WriteLine("                ");
                Console.WriteLine("1. Новая игра");
                Console.WriteLine("2. Загрузить игру");
                Console.WriteLine("3. Таблица рекордов");
                Console.WriteLine("4. Правила игры");
                Console.WriteLine("5. Выход");
                Console.WriteLine("                 ");
                Console.WriteLine("Выберите пункт от 1 до 5");

                string input = Console.ReadLine()?.Trim(); //.Trim убирает случайные пробелы

                switch(input)
                {
                    case "1" : return MainMenuAction.NewGame;
                    case "2" : return MainMenuAction.LoadGame;
                    case "3" : return MainMenuAction.Records;
                    case "4" : return MainMenuAction.Rules;
                    case "5" : return MainMenuAction.Exit;
                    default:
                        Console.WriteLine("\nОшибка: неверный ввод. Нажмите на любую клавишу");
                        Console.ReadKey();
                        break;
                }
            } 
        }

        private void PrintCentered(string text)
        {
            if (text.Length >= ConsoleWidth)
            {
                Console.WriteLine(text);
                return;
            }
            int padLeft = (ConsoleWidth - text.Length) / 2;
            Console.WriteLine(new string(' ', padLeft) + text);
        }
    }
}