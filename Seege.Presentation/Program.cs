using Seege.Presentation.Contracts;
using System;

namespace Seege.Presentation
{
    /// <summary>
    /// Запускает консольное приложение и обрабатывает выбор в главном меню.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Точка входа: показывает меню и запускает выбранный экран.
        /// </summary>
        /// <param name="args">Аргументы командной строки.</param>
        static void Main(string[] args)
        {
            MainMenu menu = new MainMenu();
            GameRenderer renderer = new GameRenderer();
            InputHandler inputHandler = new InputHandler();

            while (true)
            {
                MainMenuAction action = menu.ShowMainMenu();

                switch (action)
                {
                    case MainMenuAction.NewGame:
                        StartMockGame(renderer, inputHandler);
                        break;
                    
                    case MainMenuAction.LoadGame:
                        ShowStubMessage("Загрузка игры...."); //будет реализовано на 3 неделе
                        break;

                    case MainMenuAction.Records:
                        ShowStubMessage("Таблица рекордов"); // будет реализовано на 5 неделе
                        break;

                    case MainMenuAction.Rules:
                        ShowRules(renderer);
                        break;

                    case MainMenuAction.Exit:
                        return;


                }
            }
        }
        // временная заглушка для тестов
        /// <summary>
        /// Запускает демонстрационный сценарий с тестовым состоянием поля.
        /// </summary>
        /// <param name="renderer">Отрисовщик игрового поля.</param>
        /// <param name="inputhandler">Обработчик пользовательского ввода.</param>
        static void StartMockGame(GameRenderer renderer, InputHandler inputhandler)
        {
            var(p1,p2) = inputhandler.GetPlayerNames(); //получение имени
            // тестовое состояние игры
            var state = new GameState
            {
                CurrentPhase = GamePhase.Placement,
                ActivePlayer = PlayerId.Player1,
                MoveNumber = 1,
                Player1Name = p1,
                Player2Name = p2
            };

            //рисуем доску
            renderer.RenderBoard(state);
            Console.WriteLine("Тестовый ввод координат (нажмите Enter для выхода в меню):");
            Move testMove = inputHandler.GetMoveFromUser();
            Console.WriteLine($"Вы ввели: строка {testMove.ToRow + 1}, столбец {testMove.ToCol + 1}");

            Console.WriteLine("Выход");
            Console.ReadKey();

        }

        /// <summary>
        /// Показывает правила игры и возвращает пользователя в меню после нажатия клавиши.
        /// </summary>
        /// <param name="renderer">Отрисовщик текста правил.</param>
        static void ShowRules(GameRenderer renderer) // метод принимает  gamerenderer c правилами
        {
            Console.Clear();
            renderer.RenderRules();
            Console.WriteLine("Для возврата нажмите на любую кнопку");
            Console.ReadKey();
        }

        /// <summary>
        /// Показывает временное сообщение и ждёт нажатия клавиши.
        /// </summary>
        /// <param name="message">Текст, который нужно показать.</param>
        static void ShowStubMessage(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            Console.WriteLine("Выход");
            Console.ReadKey();
        }
    }
}