using Seega.Contracts;

namespace Seega.Business;

public class Board
{
    public const int Size = 5;
    public static readonly BoardCoordinate CenterCell = new(2, 2); // C3 = Крепость (индекс 12)

    private readonly PlayerId[] _cells = new PlayerId[25];

    public PlayerId this[int row, int col]
    {
        get => _cells[row * Size + col];
        set => _cells[row * Size + col] = value;
    }

    public PlayerId this[BoardCoordinate coord]
    {
        get => _cells[coord.Index];
        set => _cells[coord.Index] = value;
    }

    public void Clear() => Array.Clear(_cells, 0, _cells.Length);

    public PlayerId[] GetRawBoard() => (PlayerId[])_cells.Clone();

    public void LoadFrom(PlayerId[] source) => Array.Copy(source, _cells, 25);
}