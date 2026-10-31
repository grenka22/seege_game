using System;

namespace Seega.Contracts;

public record GameRecord(
    string Player1Name,
    string Player2Name,
    string? WinnerName,
    int Score,
    int TotalTurns,
    DateTime CompletedAt
);