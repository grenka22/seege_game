namespace Seega.Contracts;

/// <summary>
/// Запись о завершенной игре для таблицы рекордов
/// </summary>
public record GameRecord(
    string WinnerName,
    string LoserName,
    int Score,
    int TotalTurns,
    DateTime CompletedAt
);