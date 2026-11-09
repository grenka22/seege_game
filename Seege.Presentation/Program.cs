using Seega.Contracts;
using Seega.Business;
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
                        StartGame(renderer, inputHandler);
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
        /// <summary>
        /// Запускает новую партию и передаёт действия игроков бизнес-логике.
        /// </summary>
        /// <param name="renderer">Отрисовщик игрового поля.</param>
        /// <param name="inputHandler">Обработчик пользовательского ввода.</param>
        static void StartGame(GameRenderer renderer, InputHandler inputHandler)
        {
            (string player1Name, string player2Name) = inputHandler.GetPlayerNames();
            GameManager gameManager = new GameManager();
            gameManager.StartNewGame(player1Name, player2Name);

            while (true)
            {
                GameState state = gameManager.GetCurrentState();
                renderer.RenderBoard(state);

                if (state.Phase == GamePhase.Finished)
                {
                    Console.WriteLine("Нажмите любую клавишу, чтобы вернуться в меню.");
                    Console.ReadKey();
                    return;
                }

                MoveResult result;
                if (state.Phase == GamePhase.Placement)
                {
                    BoardCoordinate? target = inputHandler.GetCoordinateFromUser("Куда поставить фишку");
                    if (!target.HasValue)
                        return;

                    result = gameManager.PlacePiece(target.Value);
                }
                else
                {
                    BoardCoordinate from;
                    if (state.ComboPiecePosition.HasValue)
                    {
                        from = state.ComboPiecePosition.Value;
                        Console.WriteLine($"Серия захватов: продолжайте фишкой с клетки {from}.");
                        Console.WriteLine("Нажмите P, чтобы завершить серию, или любую другую клавишу для продолжения.");

                        if (Console.ReadKey(true).Key == ConsoleKey.P)
                        {
                            result = gameManager.PassTurn();
                        }
                        else
                        {
                            BoardCoordinate? comboDestination = inputHandler.GetCoordinateFromUser("Куда переместить фишку");
                            if (!comboDestination.HasValue)
                                return;

                            result = gameManager.MakeMove(from, comboDestination.Value);
                        }
                    }
                    else
                    {
                        BoardCoordinate? selectedFrom = inputHandler.GetCoordinateFromUser("Откуда переместить фишку");
                        if (!selectedFrom.HasValue)
                            return;

                        BoardCoordinate? to = inputHandler.GetCoordinateFromUser("Куда переместить фишку");
                        if (!to.HasValue)
                            return;

                        result = gameManager.MakeMove(selectedFrom.Value, to.Value);
                    }
                }

                if (!result.IsSuccess)
                {
                    renderer.RenderMoveError(result.ErrorReason);
                    Console.WriteLine("Нажмите любую клавишу, чтобы продолжить.");
                    Console.ReadKey();
                    continue;
                }

                state = gameManager.GetCurrentState();
                renderer.RenderBoard(state);
                renderer.RenderMoveResult(result, state);
                if (result.IsGameOver)
                {
                    Console.WriteLine("Нажмите любую клавишу, чтобы вернуться в меню.");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("Нажмите любую клавишу, чтобы продолжить.");
                Console.ReadKey();
            }
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