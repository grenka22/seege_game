using Seege.Presentation.Contracts;
using System;

namespace Seege.Presentation
{
    class Program
    {
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
                        ShowRules();
                        break;
                    
                    case MainMenuAction.Exit:
                        return;
                }
            }
        }
        // временная заглушка для тестов
        static void StartMockGame(GameRenderer renderer, Inputhandler inputhandler)
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

            //добавление фишек для визуала
            state.Board[2, 2] = PlayerId.Player1;
            state.Board[2, 0] = PlayerId.Player2;

            //рисуем доску
            renderer.RenderBoard(state);
            Console.WriteLine("Тестовый ввод координат (нажмите Enter для выхода в меню):");
            Move testMove = inputHandler.GetMoveFromUser();
            Console.WriteLine($"Вы ввели: строка {testMove.ToRow + 1}, столбец {testMove.ToCol + 1}");

            Console.WriteLine("Выход");
            Console.ReadKey();

        }

        static void ShowRules()
        {
            Console.Clear();
            Console.WriteLine("Правила игры SEEGE");
             Console.WriteLine("1. Игроки по очереди расставляют фишки.");
            Console.WriteLine("2. После расстановки фишки перемещаются на 1 клетку.");
            Console.WriteLine("3. Захват фишек соперника при окружении.");
            Console.WriteLine("\n(Полные правила будут добавлены позже)");
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        
        }

        static void ShowStubMessage(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            Console.WriteLine("Выход");
            Console.ReadKey();
        }
    }
}