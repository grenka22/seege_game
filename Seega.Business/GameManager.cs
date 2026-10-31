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