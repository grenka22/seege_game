namespace Seega.Contracts;

/// <summary>
/// Объект-результат (паттерн Result Object), который бизнес-логика (BL) возвращает интерфейсу (UI) 
/// после любой попытки сделать ход или поставить фишку.
/// Содержит полную информацию: успешен ли ход, какие фишки съедены, кто ходит дальше и не окончена ли игра.
/// </summary>
public class MoveResult
{

    /// <summary>
    /// Флаг успешности действия:
    /// True — ход был легален и применен к полю;
    /// False — ход нарушил правила игры (см. ErrorReason).
    /// </summary>
    public bool IsSuccess { get; set; }

    /// <summary>
    /// Конкретная причина ошибки, если IsSuccess == False.
    /// Позволяет UI показать игроку понятный текст ошибки (например, "Клетка занята").
    /// </summary>
    public MoveErrorReason ErrorReason { get; set; }

    /// <summary>
    /// Текущая фаза игры после совершения хода (Placement, Movement или Finished).
    /// </summary>
    public GamePhase CurrentPhase { get; set; }

    /// <summary>
    /// Игрок, чей черед ходить наступил после выполнения этого действия.
    /// </summary>
    public PlayerId CurrentPlayer { get; set; }

    /// <summary>
    /// Список координат фишек противника, которые были захвачены ("съедены") за этот ход.
    /// UI использует этот список для красивого уведомления или подсветки удаленных фишек.
    /// </summary>
    public IReadOnlyList<BoardCoordinate> CapturedPieces { get; set; } = Array.Empty<BoardCoordinate>();

    /// <summary>
    /// Флаг комбо-серии: True, если ход привел к взятию, и ЭТА ЖЕ фишка может сделать 
    /// следующий ход с еще одним взятием. Ход не переходит к сопернику.
    /// </summary>
    public bool CanContinueCombo { get; set; }

    /// <summary>
    /// Признак завершения партии (True, если игра окончена победой или ничьей).
    /// </summary>
    public bool IsGameOver { get; set; }

    /// <summary>
    /// Победитель партии (Player1 или Player2).
    /// Имеет значение null, если игра еще продолжается или завершилась вничью.
    /// </summary>
    public PlayerId? Winner { get; set; }

    /// <summary>
    /// Фабричный вспомогательный метод для быстрого создания объекта ОШИБКИ хода.
    /// </summary>
    /// <param name="reason">Причина, по которой ход не может быть совершен.</param>
    /// <param name="state">Текущее состояние игры (для копирования фазы и активного игрока).</param>
    /// <returns>Сформированный экземпляр MoveResult с флагом IsSuccess = False.</returns>
    public static MoveResult Fail(MoveErrorReason reason, GameState state) => new()
    {
        IsSuccess = false,
        ErrorReason = reason,
        CurrentPhase = state.Phase,
        CurrentPlayer = state.CurrentPlayer,
        IsGameOver = state.Phase == GamePhase.Finished,
        Winner = state.Winner
    };

    /// <summary>
    /// Фабричный вспомогательный метод для быстрого создания объекта УСПЕШНОГО хода.
    /// </summary>
    /// <param name="state">Актуальное состояние игры после применения хода.</param>
    /// <param name="captured">Список захваченных на этом шаге фишек (если были).</param>
    /// <param name="canContinueCombo">Доступен ли игроку повторный ход этой же фишкой.</param>
    /// <returns>Сформированный экземпляр MoveResult с флагом IsSuccess = True.</returns>
    public static MoveResult Success(GameState state, IReadOnlyList<BoardCoordinate>? captured = null, bool canContinueCombo = false) => new()
    {
        IsSuccess = true,
        ErrorReason = MoveErrorReason.None,
        CurrentPhase = state.Phase,
        CurrentPlayer = state.CurrentPlayer,
        // Если захватов не было, возвращаем пустой неизменяемый массив вместо null, чтобы избежать NullReferenceException в UI
        CapturedPieces = captured ?? Array.Empty<BoardCoordinate>(),
        CanContinueCombo = canContinueCombo,
        IsGameOver = state.Phase == GamePhase.Finished,
        Winner = state.Winner
    };
}