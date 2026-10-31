namespace Seega.Contracts;

/// <summary>
/// Результат выполнения действия (хода или расстановки)
/// </summary>
public class MoveResult
{
    public bool IsSuccess { get; set; }
    public MoveErrorReason ErrorReason { get; set; }
    public GamePhase CurrentPhase { get; set; }
    public PlayerId CurrentPlayer { get; set; }
    public IReadOnlyList<BoardCoordinate> CapturedPieces { get; set; } = Array.Empty<BoardCoordinate>();
    public bool CanContinueCombo { get; set; }
    public bool IsGameOver { get; set; }
    public PlayerId? Winner { get; set; }

    public static MoveResult Fail(MoveErrorReason reason, GameState state) => new()
    {
        IsSuccess = false,
        ErrorReason = reason,
        CurrentPhase = state.Phase,
        CurrentPlayer = state.CurrentPlayer,
        IsGameOver = state.Phase == GamePhase.Finished,
        Winner = state.Winner
    };

    public static MoveResult Success(GameState state, IReadOnlyList<BoardCoordinate>? captured = null, bool canContinueCombo = false) => new()
    {
        IsSuccess = true,
        ErrorReason = MoveErrorReason.None,
        CurrentPhase = state.Phase,
        CurrentPlayer = state.CurrentPlayer,
        CapturedPieces = captured ?? Array.Empty<BoardCoordinate>(),
        CanContinueCombo = canContinueCombo,
        IsGameOver = state.Phase == GamePhase.Finished,
        Winner = state.Winner
    };
}