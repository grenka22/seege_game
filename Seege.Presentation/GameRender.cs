//рисуем доску 5х5 и шапку с информацией
using System;
using Seega.Presentation.Contracts;

namespace Seega.Presentation
{
    public class GameRenderer
    {
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
            Console.WriteLine("  +-------------------+");;
            
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
        public void RenderRules()
        {
            Console.Clear();
            Console.WriteLine("           Правила игры SEEGA 5x5                ");
            Console.WriteLine("1. Игроки по очереди расставляют фишки на поле.");
            Console.WriteLine("2. После расстановки начинается фаза перемещения.");
            Console.WriteLine("3. Фишки ходят на 1 клетку по горизонтали или вертикали.");
            Console.WriteLine("4. Захват фишек соперника происходит при их окружении.");
            Console.WriteLine("\nДля возврата в меню нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}