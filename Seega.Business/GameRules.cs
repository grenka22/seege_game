using Seega.Contracts;

namespace Seega.Business;

public static class GameRules
{
    public static bool IsValidPlacement(Board board, BoardCoordinate target, out MoveErrorReason error)
    {
        if (target.Row == 2 && target.Col == 2)
        {
            error = MoveErrorReason.CenterRestrictedInPlacement;
            return false;
        }

        if (board[target] != PlayerId.None)
        {
            error = MoveErrorReason.CellOccupied;
            return false;
        }

        error = MoveErrorReason.None;
        return true;
    }
}