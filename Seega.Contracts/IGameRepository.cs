namespace Seega.Contracts;

/// <summary>
/// Интерфейс сохранения и загрузки партии
/// </summary>
public interface IGameRepository
{
    void SaveGame(GameState state);
    GameState LoadGame();
    bool HasSavedGame();
    void DeleteSave();
}