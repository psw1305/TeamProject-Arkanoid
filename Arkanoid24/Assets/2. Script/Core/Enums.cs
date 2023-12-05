
/// <summary>
/// 게임 진행 타입
/// </summary>
public enum GameState
{
    Play,
    Pause,
}

/// <summary>
/// 게임 아이템 타입
/// </summary>
public enum Items
{
    None,
    Player,
    Lasers,
    Enlarge,
    Catch,
    Slow,
    Disruption,
    Power,
}

/// <summary>
/// 게임 모드 나타내는 타입
/// </summary>
public enum GameMode
{
    None,
    Main,
    TimeAttack,
}
