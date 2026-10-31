using Seega.Contracts;

namespace Seega.Business;

public class Player
{
    public PlayerId Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int PiecesToPlace { get; set; } = 12;
    public int PiecesOnBoard { get; set; } = 0;
}