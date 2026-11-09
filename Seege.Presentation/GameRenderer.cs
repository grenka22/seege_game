//рисуем доску 5х5 и шапку с информацией
using System;
using Seega.Contracts;

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
            Console.WriteLine($"Фаза: {GetPhaseName(state.Phase)} | Ход №{state.TurnNumber}");
            Console.WriteLine($"Сейчас ходит: {(state.CurrentPlayer == PlayerId.Player1 ?
            state.Player1Name : state.Player2Name)}");

            int p1Count = CountChips(state, PlayerId.Player1);
            int p2Count = CountChips(state, PlayerId.Player2);
            Console.WriteLine($"{state.Player1Name} (X): {p1Count} фишек");
            Console.WriteLine($"{state.Player2Name} (O): {p2Count} фишек\n");

            Console.WriteLine("     A   B   C   D   E");
            Console.WriteLine("   +---+---+---+---+---+");

            for (int row = 0; row < 5; row++)
            {
                Console.Write($"{row + 1}  |");
                for (int col = 0; col < 5; col++)
                {
                    Console.Write($" {GetCellSymbol(state.GetCell(row, col))} |");
                }
                Console.WriteLine();
                Console.WriteLine("   +---+---+---+---+---+");
            }

            PrintCentered("Обозначения: X - Игрок 1, O - Игрок 2, . - пустая клетка");
        }

        private int CountChips(GameState state, PlayerId player)
        {
            int count = 0;
            foreach (PlayerId cell in state.Board)
            {
                if (cell == player)
                    count++;
            }

            return count;
        }

        private string GetPhaseName(GamePhase phase)
        {
            return phase switch
            {
                GamePhase.Placement => "Расстановка",
                GamePhase.Movement => "Перемещение",
                GamePhase.Finished => "Игра окончена",
                _=>"Неизвестно"
            };
        }

        /// <summary>
        /// Показывает понятное сообщение о причине отклонённого действия.
        /// </summary>
        /// <param name="reason">Причина, возвращённая бизнес-логикой.</param>
        public void RenderMoveError(MoveErrorReason reason)
        {
            string message = reason switch
            {
                MoveErrorReason.None => "Действие не выполнено. Попробуйте ещё раз.",
                MoveErrorReason.OutOfBounds => "Координаты находятся за пределами поля.",
                MoveErrorReason.CellOccupied => "Эта клетка уже занята.",
                MoveErrorReason.CenterRestrictedInPlacement => "Во время расстановки нельзя занимать центр поля.",
                MoveErrorReason.NotPlayerPiece => "Выберите свою фишку.",
                MoveErrorReason.DestinationOccupied => "Клетка назначения занята.",
                MoveErrorReason.InvalidStepRange => "Можно переместить фишку только на одну клетку по горизонтали или вертикали.",
                MoveErrorReason.PieceHasNoMoves => "Эта фишка не может двигаться.",
                MoveErrorReason.MustContinueComboWithSamePiece => "Продолжите серию той же фишкой или завершите её.",
                MoveErrorReason.GameAlreadyFinished => "Партия уже завершена.",
                MoveErrorReason.NoMovesToPass => "Сейчас нельзя пропустить ход.",
                _ => "Действие отклонено."
            };

            Console.WriteLine(message);
        }

        /// <summary>
        /// Показывает сведения о захватах и завершении партии.
        /// </summary>
        /// <param name="result">Результат действия, возвращённый бизнес-логикой.</param>
        /// <param name="state">Актуальное состояние игры.</param>
        public void RenderMoveResult(MoveResult result, GameState state)
        {
            if (result.CapturedPieces.Count > 0)
                Console.WriteLine($"Захвачено фишек: {result.CapturedPieces.Count}.");

            if (result.CanContinueCombo)
                Console.WriteLine("Можно продолжить серию захватов той же фишкой.");

            if (!result.IsGameOver)
                return;

            if (result.Winner.HasValue && result.Winner.Value != PlayerId.None)
            {
                string winnerName = result.Winner.Value == PlayerId.Player1
                    ? state.Player1Name
                    : state.Player2Name;
                Console.WriteLine($"Победил игрок {winnerName}!");
            }
            else
            {
                Console.WriteLine("Партия завершилась вничью.");
            }
        }

        // метод для отображения правил
        /// <summary>
        /// Выводит краткие правила игры.
        /// </summary>
        public void RenderRules()
        {
            Console.WriteLine("           Правила игры SEEGA 5x5                ");
            Console.WriteLine("1. Игроки по очереди выставляют по две фишки за ход.");
            Console.WriteLine("2. Во время расстановки центральная клетка C3 закрыта.");
            Console.WriteLine("3. После расстановки начинается фаза перемещения.");
            Console.WriteLine("4. Фишки ходят на одну клетку по горизонтали или вертикали.");
            Console.WriteLine("5. Фишки соперника захватываются, если оказываются между вашими.");
            Console.WriteLine("\nДля возврата в меню нажмите любую клавишу...");
            Console.WriteLine();
        }

        private string GetCellSymbol(PlayerId cell)
        {
            if ( cell == PlayerId.Player1) return "X";
            if ( cell == PlayerId.Player2) return "O";
            return ".";
        }

        private void PrintCentered(string text)
        {
            if (text.Length >= ConsoleWidth)
            {
                Console.WriteLine(text);
                return;
            }

            int padding = (ConsoleWidth - text.Length) / 2;
            Console.WriteLine(new string(' ', padding) + text);
        }
    }
}