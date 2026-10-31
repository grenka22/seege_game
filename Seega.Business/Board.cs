using System;
using Seega.Contracts;

namespace Seega.Business;

public class Board
{
    public const int Size = 5;
    public static readonly BoardCoordinate CenterCell = new(2, 2);

    private readonly PlayerId[] _cells = new PlayerId[25];

    public static bool IsInBounds(int row, int col) => row is >= 0 and < Size && col is >= 0 and < Size;

    public PlayerId this[int row, int col]
    {
        get
        {
            if (!IsInBounds(row, col))
                throw new ArgumentOutOfRangeException($"Координаты [{row}, {col}] выходят за границы поля 5х5.");
            return _cells[row * Size + col];
        }
        set
        {
            if (!IsInBounds(row, col))
                throw new ArgumentOutOfRangeException($"Координаты [{row}, {col}] выходят за границы поля 5х5.");
            _cells[row * Size + col] = value;
        }
    }

    public PlayerId this[BoardCoordinate coord]
    {
        get => this[coord.Row, coord.Col];
        set => this[coord.Row, coord.Col] = value;
    }

    public void Clear() => Array.Clear(_cells, 0, _cells.Length);

    public PlayerId[] GetRawBoard() => (PlayerId[])_cells.Clone();

    public void LoadFrom(PlayerId[] source) => Array.Copy(source, _cells, 25);
}