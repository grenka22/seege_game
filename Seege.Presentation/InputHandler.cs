//ввод координат и обработка ошибок
using System;
using Seege.Presentation.Contracts;

namespace Seege.Presentation
{
    public class InputHandler
    {
        //запрос координатов у игрока и возвращает обьект move
        public Move GetMoveFromUser()
        {
            while (true)
            {
                Console.WriteLine("Введите координаты столбца");
                string input = Console.ReadLine();
                
                //разбитие ввода по пробелам
                string[] parts = input?.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts?.Length == 2)
                {
                    bool isRowValid = int.TryParse(parts[0], out int row);
                    bool isColValid = int.TryParse(parts[1], out int col);

                    if (isRowValid && isColValid && row >= 1 && row <= 5 && col >= 1 && col <= 5)
                    {
                        return new Move { ToRow = row - 1, ToCol = col - 1};
                    }
                }
                Console.WriteLine("Ошибка. Нужно вводить числа через пробел '2 4' ");
            }
        }
        public(string p1Name, string p2Name) GetPlayerName()
        {
            Console.Clear();
            Console.Write("Введите имя первого игрока: ");
            string p1 = Console.ReadLine();

            Console.Write("Введите имя второго игрока: ");
            string p2 = Console.ReadLine();

            if ( string.IsNullOrWhiteSpace(p1)) p1 = "Игрок 1 ";
            if ( string.IsNullOrWhiteSpace(p2)) p2 = "Игрок 2 ";

            return(p1, p2);

        }
    }
}