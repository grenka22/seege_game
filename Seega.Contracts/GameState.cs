using System;
using System.Collections.Generic;

namespace Seega.Contracts;

public class GameState
{
    public PlayerId[] Board { get; set; } = new PlayerId[25];

    public GamePhase Phase { get; set; } = GamePhase.Placement;
    public PlayerId CurrentPlayer { get; set; } = PlayerId.Player1;

    public string Player1Name { get; set; } = "Player 1";
    public string Player2Name { get; set; } = "Player 2";

    public int Player1PiecesToPlace { get; set; } = 12;
    public int Player2PiecesToPlace { get; set; } = 12;

    public int Player1PiecesOnBoard { get; set; } = 0;
    public int Player2PiecesOnBoard { get; set; } = 0;

    public int PlacementSubTurn { get; set; } = 1;
    public int TurnNumber { get; set; } = 1;

    public BoardCoordinate? ComboPiecePosition { get; set; }
    public PlayerId? Winner { get; set; }
    public List<string> MoveHistory { get; set; } = new();

    public GameState Clone()
    {
        var clone = (GameState)this.MemberwiseClone();
        clone.Board = (PlayerId[])this.Board.Clone();
        clone.MoveHistory = new List<string>(this.MoveHistory); // Защита истории от перезаписи
        return clone;
    }

    public PlayerId GetCell(int row, int col) => Board[row * 5 + col];
    public void SetCell(int row, int col, PlayerId player) => Board[row * 5 + col] = player;
}