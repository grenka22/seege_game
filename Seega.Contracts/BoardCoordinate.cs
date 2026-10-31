using System;

namespace Seega.Contracts;

public readonly record struct BoardCoordinate(int Row, int Col)
{
    public int Index => Row * 5 + Col;

    public static BoardCoordinate FromIndex(int index) => new(index / 5, index % 5);

    public static bool TryParse(string? input, out BoardCoordinate coordinate)
    {
        coordinate = default;
        if (string.IsNullOrWhiteSpace(input)) return false;

        string clean = input.Trim().ToUpperInvariant();
        if (clean.Length != 2) return false;

        char colChar = clean[0];
        char rowChar = clean[1];

        // Поддержка русской раскладки (А, В, С, D/Д, Е)
        colChar = colChar switch
        {
            'А' => 'A',
            'В' => 'B',
            'С' => 'C',
            'D' or 'Д' => 'D',
            'Е' => 'E',
            _ => colChar
        };

        int col = colChar - 'A';
        int row = rowChar - '1';

        if (col is < 0 or > 4 || row is < 0 or > 4) return false;

        coordinate = new BoardCoordinate(row, col);
        return true;
    }

    public override string ToString() => $"{(char)('A' + Col)}{Row + 1}";
}