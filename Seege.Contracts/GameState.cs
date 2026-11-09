using System;
using System.Collections.Generic;

namespace Seega.Contracts;

/// <summary>
/// Представляет полный снимок текущего состояния игры.
/// Содержит все данные, необходимые для отрисовки в UI, сохранения в JSON и отмены ходов (Undo).
/// Класс не содержит игровой логики, только чистые данные.
/// </summary>
public class GameState
{
    /// <summary>
    /// Игровое поле 5x5 в виде одномерного плоского массива из 25 клеток (индексы от 0 до 24).
    /// Использование одномерного массива обеспечивает 100% совместимость с сериализацией System.Text.Json.
    /// Индекс клетки вычисляется по формуле: row * 5 + col.
    /// </summary>
    public PlayerId[] Board { get; set; } = new PlayerId[25];

    /// <summary>
    /// Текущая фаза игры: Placement (расстановка), Movement (движение) или Finished (партия окончена).
    /// </summary>
    public GamePhase Phase { get; set; } = GamePhase.Placement;

    /// <summary>
    /// Игрок, чья очередь делать ход в данный момент (Player1 или Player2).
    /// </summary>
    public PlayerId CurrentPlayer { get; set; } = PlayerId.Player1;

    /// <summary>
    /// Отображаемое имя первого игрока (играет за белые фишки -> X).
    /// </summary>
    public string Player1Name { get; set; } = "Player 1";

    /// <summary>
    /// Отображаемое имя второго игрока (играет за чёрные фишки -> O).
    /// </summary>
    public string Player2Name { get; set; } = "Player 2";

    /// <summary>
    /// Количество фишек в запасе у Игрока 1, которые еще предстоит выставить на доску в фазе расстановки (от 12 до 0).
    /// </summary>
    public int Player1PiecesToPlace { get; set; } = 12;

    /// <summary>
    /// Количество фишек в запасе у Игрока 2, которые еще предстоит выставить на доску в фазе расстановки (от 12 до 0).
    /// </summary>
    public int Player2PiecesToPlace { get; set; } = 12;

    /// <summary>
    /// Текущее количество живых фишек Игрока 1 на игровом поле.
    /// </summary>
    public int Player1PiecesOnBoard { get; set; } = 0;

    /// <summary>
    /// Текущее количество живых фишек Игрока 2 на игровом поле.
    /// </summary>
    public int Player2PiecesOnBoard { get; set; } = 0;

    /// <summary>
    /// Микро-шаг в фазе расстановки (1 или 2).
    /// По правилам Сеги каждый игрок за один ход ставит ровно 2 фишки подряд.
    /// Значение 1 — ставится первая фишка пары; 2 — ставится вторая фишка пары.
    /// </summary>
    public int PlacementSubTurn { get; set; } = 1;

    /// <summary>
    /// Общий порядковый номер хода в партии (начиная с 1).
    /// </summary>
    public int TurnNumber { get; set; } = 1;

    /// <summary>
    /// Координаты фишки, совершившей захват, если для неё доступна серия повторных взятий (комбо).
    /// Если серия не активна — содержит null.
    /// </summary>
    public BoardCoordinate? ComboPiecePosition { get; set; }

    /// <summary>
    /// Победитель партии.
    /// null — партия еще идет;
    /// PlayerId.None — ничья;
    /// PlayerId.Player1 или PlayerId.Player2 — победа соответствующего игрока.
    /// </summary>
    public PlayerId? Winner { get; set; }

    /// <summary>
    /// Протокол (история) всех совершенных ходов партии в текстовом виде (например, "1. Игрок 1: A1 -> A2").
    /// Требуется для сохранения в файл и просмотра истории.
    /// </summary>
    public List<string> MoveHistory { get; set; } = new();

    /// <summary>
    /// Создает независимую глубокую копию текущего состояния.
    /// Важно для реализации отмены хода (Undo) на 6-й неделе:
    /// гарантирует, что изменение массива Board или списка MoveHistory в будущем 
    /// не испортит предыдущие снимки, сохраненные в стеке истории.
    /// </summary>
    /// <returns>Новый экземпляр GameState с полностью скопированными данными.</returns>
    public GameState Clone()
    {
        // 1. Копируем простые свойства (числа, строки, enum)
        var clone = (GameState)this.MemberwiseClone();

        // 2. Создаем отдельную копию массива доски в новой области памяти
        clone.Board = (PlayerId[])this.Board.Clone();

        // 3. Создаем отдельную копию списка истории
        clone.MoveHistory = new List<string>(this.MoveHistory); // Защита истории от перезаписи

        return clone;
    }

    /// <summary>
    /// Удобный вспомогательный метод для чтения состояния клетки по строке и колонке (от 0 до 4).
    /// </summary>
    /// <param name="row">Индекс строки (от 0 до 4).</param>
    /// <param name="col">Индекс колонки (от 0 до 4).</param>
    /// <returns>Идентификатор фишки в клетке (None, Player1 или Player2).</returns>
    public PlayerId GetCell(int row, int col) => Board[row * 5 + col];

    /// <summary>
    /// Удобный вспомогательный метод для записи фишки в клетку по строке и колонке (от 0 до 4).
    /// </summary>
    /// <param name="row">Индекс строки (от 0 до 4).</param>
    /// <param name="col">Индекс колонки (от 0 до 4).</param>
    /// <param name="player">Игрок, чья фишка выставляется в клетку.</param>
    public void SetCell(int row, int col, PlayerId player) => Board[row * 5 + col] = player;
}