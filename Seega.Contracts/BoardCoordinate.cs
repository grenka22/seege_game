using System;

namespace Seega.Contracts;

/// <summary>
/// Неизменяемые координаты клетки на поле 5х5 (строка Row и колонка Col от 0 до 4).
/// </summary>

public readonly record struct BoardCoordinate(int Row, int Col)
{
    /// <summary>
    /// Индекс клетки в плоском массиве (0..24).
    /// Используется для хранения доски в GameState и сериализации в JSON.
    /// </summary>
    public int Index => Row * 5 + Col;

    /// <summary>
    /// Создает координаты (Row, Col) на основе одномерного индекса клетки (0..24).
    /// </summary>
    public static BoardCoordinate FromIndex(int index) => new(index / 5, index % 5);

    /// <summary>
    /// Преобразует текстовый ввод игрока (например, "A1", "C3") в структуру координат.
    /// </summary>
    public static bool TryParse(string? input, out BoardCoordinate coordinate)
    {
        coordinate = default;

        // Отсекаем пустой ввод
        if (string.IsNullOrWhiteSpace(input)) return false;

        // Удаляем случайные пробелы по краям и переводим в верхний регистр
        string clean = input.Trim().ToUpperInvariant();

        // Формат строго 2 символа: буква (колонка) и цифра (строка)
        if (clean.Length != 2) return false;

        char colChar = clean[0];
        char rowChar = clean[1];

        // Вычисляем числовые индексы (A..E -> 0..4, 1..5 -> 0..4)
        int col = colChar - 'A';
        int row = rowChar - '1';

        // Проверяем, что индексы не выходят за границы доски 5х5
        if (col is < 0 or > 4 || row is < 0 or > 4) return false;

        coordinate = new BoardCoordinate(row, col);
        return true;
    }

    /// <summary>
    /// Возвращает строковое представление координаты для консоли (например, "C3").
    /// </summary>
    public override string ToString() => $"{(char)('A' + Col)}{Row + 1}";
}