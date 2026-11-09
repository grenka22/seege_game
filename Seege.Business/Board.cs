using System;
using Seega.Contracts;

namespace Seega.Business;

/// <summary>
/// Представляет игровое поле настольной игры Сега размером 5x5.
/// Инкапсулирует внутреннее состояние клеток, проверяет границы поля
/// и обеспечивает безопасный доступ к ячейкам.
/// </summary>
public class Board
{
    /// <summary>
    /// Размер стороны квадратного поля (5 клеток).
    /// </summary>
    public const int Size = 5;

    /// <summary>
    /// Координаты центральной клетки "Крепость" (Hisn) — C3, индексы [2, 2].
    /// Это особое поле: в него нельзя ставить фишки на фазе расстановки,
    /// а на фазе движения фишка в центре защищена от захвата.
    /// </summary>
    public static readonly BoardCoordinate CenterCell = new(2, 2);

    /// <summary>
    /// Внутреннее хранилище клеток поля.
    /// Используется одномерный массив фиксированного размера 25 (5 * 5),
    /// где ячейка со значением PlayerId.None означает пустую клетку.
    /// </summary>
    private readonly PlayerId[] _cells = new PlayerId[25];

    /// <summary>
    /// Проверяет, попадают ли переданные строка и колонка в границы игрового поля [от 0 до 4].
    /// Метод защищает от ошибок выхода за пределы массива и некорректных математических смещений.
    /// </summary>
    /// <param name="row">Индекс строки (от 0 до 4).</param>
    /// <param name="col">Индекс столбца (от 0 до 4).</param>
    /// <returns>True, если координаты лежат в пределах сетки 5х5; иначе False.</returns>
    public static bool IsInBounds(int row, int col) => row is >= 0 and < Size && col is >= 0 and < Size;

    /// <summary>
    /// Индексатор для доступа к клетке поля по числовым индексам строки и столбца.
    /// Предоставляет удобный синтаксис вида: board[row, col].
    /// </summary>
    /// <param name="row">Индекс строки (от 0 до 4).</param>
    /// <param name="col">Индекс столбца (от 0 до 4).</param>
    /// <exception cref="ArgumentOutOfRangeException">Выбрасывается, если координаты выходят за пределы сетки 5x5.</exception>
    public PlayerId this[int row, int col]
    {
        get
        {
            // Строго проверяем координаты перед чтением, чтобы избежать ложных заворотов за край поля
            if (!IsInBounds(row, col))
                throw new ArgumentOutOfRangeException($"Координаты [{row}, {col}] выходят за границы поля 5х5.");
    
            // Преобразуем двумерные координаты в плоский индекс массива
            return _cells[row * Size + col];
        }
        set
        {
            // Проверяем границы перед записью нового состояния клетки
            if (!IsInBounds(row, col))
                throw new ArgumentOutOfRangeException($"Координаты [{row}, {col}] выходят за границы поля 5х5.");
            _cells[row * Size + col] = value;
        }
    }

    /// <summary>
    /// Перегрузка индексатора для прямого обращения через объект структуры BoardCoordinate.
    /// Позволяет писать код вида: board[coord] вместо board[coord.Row, coord.Col].
    /// </summary>
    /// <param name="coord">Координата клетки.</param>
    public PlayerId this[BoardCoordinate coord]
    {
        get => this[coord.Row, coord.Col];
        set => this[coord.Row, coord.Col] = value;
    }

    /// <summary>
    /// Полностью очищает поле, делая все 25 ячеек пустыми (PlayerId.None).
    /// Вызывается при запуске новой партии.
    /// </summary>
    public void Clear() => Array.Clear(_cells, 0, _cells.Length);

    /// <summary>
    /// Создает и возвращает независимую копию (снимок) текущего состояния поля.
    /// Важно: возвращается копия (.Clone()), чтобы внешний код (UI или DAL)
    /// не мог случайно испортить внутреннее состояние массива доски.
    /// </summary>
    /// <returns>Массив из 25 элементов с текущим расположением фишек.</returns>
    public PlayerId[] GetRawBoard() => (PlayerId[])_cells.Clone();

    /// <summary>
    /// Восстанавливает расположение фишек на доске из переданного внешнего массива.
    /// Используется при загрузке сохраненной игры или отмене хода (Undo).
    /// </summary>
    /// <param name="source">Массив из 25 элементов с сохраненным состоянием.</param>
    public void LoadFrom(PlayerId[] source) => Array.Copy(source, _cells, 25);
}