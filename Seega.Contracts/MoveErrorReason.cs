namespace Seega.Contracts;

/// <summary>
/// Причины отклонения хода
/// </summary>
public enum MoveErrorReason : byte
{
    None = 0,
    OutOfBounds,
    CellOccupied,
    CenterRestrictedInPlacement,
    NotPlayerPiece,
    DestinationOccupied,
    InvalidStepRange,
    PieceHasNoMoves,
    MustContinueComboWithSamePiece,
    GameAlreadyFinished,
    NoMovesToPass
}