namespace Seega.Contracts;

/// <summary>
/// Интерфейс таблицы рекордов
/// </summary>
public interface IRecordRepository
{
    void AddRecord(GameRecord record);
    IReadOnlyList<GameRecord> GetTopRecords(int count);
}