using Seega.Contracts;

namespace Seega.Business;

public class GameManager
{
    private readonly Board _board = new();
    private GameState _state = new();

    public void StartNewGame(string p1Name, string p2Name)
    {
        _board.Clear();
        _state = new GameState
        {
            Player1Name = p1Name,
            Player2Name = p2Name,
            Phase = GamePhase.Placement,
            CurrentPlayer = PlayerId.Player1,
            PlacementSubTurn = 1,
            Player1PiecesToPlace = 12,
            Player2PiecesToPlace = 12
        };
    }

    // Заглушка для фазы расстановки
    public MoveResult PlacePiece(BoardCoordinate target)
    {
        return MoveResult.Fail(MoveErrorReason.None, _state);
    }

    // Заглушка для фазы движения
    public MoveResult MakeMove(BoardCoordinate from, BoardCoordinate to)
    {
        return MoveResult.Fail(MoveErrorReason.None, _state);
    }

    // Заглушка для пропуска хода при блокаде или завершения комбо
    public MoveResult PassTurn()
    {
        return MoveResult.Fail(MoveErrorReason.None, _state);
    }

    // Заглушка для отмены хода
    public bool Undo()
    {
        return false;
    }

    public GameState GetCurrentState()
    {
        _state.Board = _board.GetRawBoard();
        return _state.Clone();
    }

    public void LoadState(GameState state)
    {
        _state = state.Clone();
        _board.LoadFrom(_state.Board);
    }
}