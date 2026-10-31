namespace Seega.Contracts;

/// <summary>
/// Координата клетки поля 5x5
/// </summary>
public readonly record struct BoardCoordinate(int Row, int Col)
{
    public int Index => Row * 5 + Col;

    public static BoardCoordinate FromIndex(int index) => new(index / 5, index % 5);

    /// <summary>
    /// Парсит строку вида "A1", "C3", "e5" в координаты
    /// </summary>
    public static bool TryParse(string? input, out BoardCoordinate coordinate)
    {
        coordinate = default;
        if (string.IsNullOrWhiteSpace(input) || input.Length != 2) return false;

        char colChar = char.ToUpperInvariant(input[0]);
        char rowChar = input[1];

        int col = colChar - 'A';
        int row = rowChar - '1';

        if (col is < 0 or > 4 || row is < 0 or > 4) return false;

        coordinate = new BoardCoordinate(row, col);
        return true;
    }

    public override string ToString() => $"{(char)('A' + Col)}{Row + 1}";
}