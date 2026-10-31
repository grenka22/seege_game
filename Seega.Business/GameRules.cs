using Seega.Contracts;

namespace Seega.Business;

/// <summary>
/// Статический класс, содержащий чистые правила игры Сега.
/// Не хранит состояние, отвечает исключительно за логическую валидацию действий игроков.
/// </summary>
public static class GameRules
{
    /// <summary>
    /// Проверяет допустимость выставления фишки на указанную клетку во время Фазы 1 (Фаза расстановки).
    /// </summary>
    /// <param name="board">Текущее игровое поле с расставленными фишками.</param>
    /// <param name="target">Координата клетки, куда игрок пытается поставить фишку.</param>
    /// <param name="error">Выходной параметр: детальная причина ошибки, если ход недопустим. 
    /// При успешной проверке равен MoveErrorReason.None.</param>
    /// <returns>True, если фишку можно поставить в указанную клетку; иначе False.</returns>
    public static bool IsValidPlacement(Board board, BoardCoordinate target, out MoveErrorReason error)
    {
        // Правило 1: Центральная клетка C3 (индекс [2, 2], "Крепость")
        // По правилам игры Сега центр обязан оставаться пустым на протяжении всей фазы расстановки.
        if (target.Row == 2 && target.Col == 2)
        {
            error = MoveErrorReason.CenterRestrictedInPlacement;
            return false;
        }

        // Правило 2: Занятость клетки
        // Фишку можно ставить только на свободную ячейку (где значение равно PlayerId.None).
        if (board[target] != PlayerId.None)
        {
            error = MoveErrorReason.CellOccupied;
            return false;
        }

        // Все проверки пройдены: клетка свободна и не является центром
        error = MoveErrorReason.None;
        return true;
    }
}