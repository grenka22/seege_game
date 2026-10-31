namespace Seega.Contracts;

/// <summary>
/// Текущая фаза партии
/// </summary>
public enum GamePhase : byte
{
    Placement = 0,  // Фаза расстановки (по 2 фишки)
    Movement = 1,   // Фаза движения и взятий
    Finished = 2    // Партия завершена
}