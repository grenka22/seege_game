using Seega.Contracts;

namespace Seega.Business;

public class Move
{
    public PlayerId Player { get; set; }
    public BoardCoordinate? From { get; set; }
    public BoardCoordinate To { get; set; }
    public List<BoardCoordinate> CapturedPieces { get; set; } = new();
}