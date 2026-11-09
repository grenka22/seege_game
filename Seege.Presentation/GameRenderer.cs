//рисуем доску 5х5 и шапку с информацией
using System;
using Seege.Presentation.Contracts;

namespace Seege.Presentation
{
    /// <summary>
    /// Выводит игровое поле и связанные с ним сведения в консоль.
    /// </summary>
    public class GameRenderer
    {
        // ширина консоли для центрирования
        private const int ConsoleWidth = 80;

        /// <summary>
        /// Очищает консоль и выводит заголовок по центру.
        /// </summary>
        /// <param name="title">Текст заголовка.</param>
        public void RenderHeader(string title)
        {
            Console.Clear();
            Console.WriteLine();
            PrintCentered("                                  ");
            PrintCentered($"       {title.ToUpper()         }");
            PrintCentered("                                  ");
            Console.WriteLine();
        }

        /// <summary>
        /// Показывает поле, фазу игры, номер хода и количество фишек у игроков.
        /// </summary>
        /// <param name="state">Текущее состояние игры.</param>
        public void RenderBoard(GameState state)
        {
            Console.Clear();
            Console.WriteLine("                                     ");
            Console.WriteLine($"Фаза: {GetPhaseName(state.CurrentPhase)} | Ход №{state.MoveNumber}");
            Console.WriteLine($"Сейчас ходит: {(state.ActivePlayer == PlayerId.Player1 ? 
            state.Player1Name : state.Player2Name)}");
            Console.WriteLine("                                      ");

            // счетчик фишек
            int p1Count = CountChips(state, PlayerId.Player1);
            int p2Count = CountChips(state, PlayerId.Player2);
            Console.WriteLine($"{state.Player1Name} (X): {p1Count} фишек");
            Console.WriteLine($"{state.Player2Name} (O): {p2Count} фишек\n");

            // рисуем доску
            Console.WriteLine("    1   2   3   4   5");
            Console.WriteLine("  +-------------------+");
            
            for (int row = 0; row < 5; row++)
            {
                Console.Write($"{row + 1} | ");
                for (int col = 0; col < 5; col++)
                {
                    Console.Write(GetCellSymbol(state.Board[row, col]) + " ");
                }
                Console.WriteLine("|");
                 Console.WriteLine("  +-----------+\n");
            }
            Console.WriteLine();
            PrintCentered("Обозначения: X - Игрок 1, O - Игрок 2, . - пустая клетка");
        }

        private int CountChips(GameState state, PlayerId player)
        {
            int count = 0;
            for (int r = 0; r < 5; r++)
            {
                for (int c = 0; c < 5; c++) 
                {
                    if (state.Board[r, c] == player) 
                        count++;
                }
            }
            return count;   
        }

        private string GetPhaseName(GamePhase phase)
        {
            return phase switch
            {
                GamePhase.Placement => "Расстановка",
                GamePhase.Movement => "Перемещение",
                GamePhase.GameOver => "Игра окончена",
                _=>"Неизвестно"
            };
        }

        // метод для отображения правил
        /// <summary>
        /// Выводит краткие правила игры.
        /// </summary>
        public void RenderRules()
        {
            Console.WriteLine("           Правила игры SEEGA 5x5                ");
            Console.WriteLine("1. Игроки по очереди расставляют фишки на поле.");
            Console.WriteLine("2. После расстановки начинается фаза перемещения.");
            Console.WriteLine("3. Фишки ходят на 1 клетку по горизонтали или вертикали.");
            Console.WriteLine("4. Захват фишек соперника происходит при их окружении.");
            Console.WriteLine("\nДля возврата в меню нажмите любую клавишу...");
            Console.WriteLine();
        }

        private string GetCellSymbol(PlayerId? cell)
        {
            if ( cell == PlayerId.Player1) return "X";
            if ( cell == PlayerId.Player2) return "O";
            return ".";
        }
    }
}