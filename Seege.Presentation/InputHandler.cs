//ввод координат и обработка ошибок
using System;
using Seega.Contracts;

namespace Seege.Presentation
{
    /// <summary>
    /// Считывает имена игроков и координаты из консоли.
    /// </summary>
    public class InputHandler
    {
        /// <summary>
        /// Запрашивает координату поля. Вводится как "строка столбец" (например, "2 4")
        /// или в шахматной записи (например, "D2"). Буква Q возвращает пользователя в меню.
        /// </summary>
        /// <param name="prompt">Текст запроса координаты.</param>
        /// <returns>Координата или null, если пользователь решил выйти из игры.</returns>
        public BoardCoordinate? GetCoordinateFromUser(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt} (или Q для возврата в меню): ");
                string? input = Console.ReadLine();

                if (input == null || input.Trim().Equals("Q", StringComparison.OrdinalIgnoreCase))
                    return null;

                string[] parts = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2
                    && int.TryParse(parts[0], out int row)
                    && int.TryParse(parts[1], out int col)
                    && row >= 1 && row <= 5
                    && col >= 1 && col <= 5)
                {
                    return new BoardCoordinate(row - 1, col - 1);
                }

                if (BoardCoordinate.TryParse(input, out BoardCoordinate coordinate))
                    return coordinate;

                Console.WriteLine("Ошибка. Введите, например, '2 4' или 'D2'.");
            }
        }

        /// <summary>
        /// Запрашивает имена обоих игроков; если имя не введено, подставляет имя по умолчанию.
        /// </summary>
        /// <returns>Имена первого и второго игроков.</returns>
        public(string p1Name, string p2Name) GetPlayerNames()
        {
            Console.Clear();
            Console.Write("Введите имя первого игрока: ");
            string p1 = Console.ReadLine()?.Trim() ?? string.Empty;

            Console.Write("Введите имя второго игрока: ");
            string p2 = Console.ReadLine()?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(p1)) p1 = "Игрок 1";
            if (string.IsNullOrWhiteSpace(p2)) p2 = "Игрок 2";

            return(p1, p2);

        }
    }
}