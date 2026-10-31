namespace Seega.Contracts;

/// <summary>
/// Полный снимок состояния игры
/// </summary>
public class GameState
{
    // Одномерный массив из 25 клеток для System.Text.Json
    public PlayerId[] Board { get; set; } = new PlayerId[25];

    public GamePhase Phase { get; set; } = GamePhase.Placement;
    public PlayerId CurrentPlayer { get; set; } = PlayerId.Player1;

    public string Player1Name { get; set; } = "Player 1";
    public string Player2Name { get; set; } = "Player 2";

    public int Player1PiecesToPlace { get; set; } = 12;
    public int Player2PiecesToPlace { get; set; } = 12;

    public int Player1PiecesOnBoard { get; set; } = 0;
    public int Player2PiecesOnBoard { get; set; } = 0;

    // 1 или 2: первая или вторая фишка текущего игрока ставится
    public int PlacementSubTurn { get; set; } = 1;

    public int TurnNumber { get; set; } = 1;

    // Координаты фишки, сделавшей захват (для комбо)
    public BoardCoordinate? ComboPiecePosition { get; set; }

    public PlayerId? Winner { get; set; }

    /// <summary>
    /// Глубокое клонирование
    /// </summary>
    public GameState Clone()
    {
        var clone = (GameState)this.MemberwiseClone();
        clone.Board = (PlayerId[])this.Board.Clone();
        return clone;
    }

    public PlayerId GetCell(int row, int col) => Board[row * 5 + col];
    public void SetCell(int row, int col, PlayerId player) => Board[row * 5 + col] = player;
}