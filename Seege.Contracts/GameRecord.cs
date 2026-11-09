using System;

namespace Seega.Contracts;

/// <summary>
/// Запись о завершённой партии для сохранения в таблицу рекордов.
/// </summary>
/// <param name="Player1Name">Имя первого участника партии.</param>
/// <param name="Player2Name">Имя второго участника партии.</param>
/// <param name="WinnerName">Имя победителя. Содержит null, если партия завершилась вничью.</param>
/// <param name="Score">Итоговые очки победителя (основной критерий сортировки).</param>
/// <param name="TotalTurns">Общее число ходов, сделанных за партию.</param>
/// <param name="CompletedAt">Дата и время завершения матча.</param>
public record GameRecord(
    string Player1Name,
    string Player2Name,
    string? WinnerName,
    int Score,
    int TotalTurns,
    DateTime CompletedAt
);