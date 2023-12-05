public class Managers : SingletonBehaviour<Managers>
{
    #region Managers Variables

    private ResourceManager resourceManager = new();
    private EventManager eventManager = new();
    private UIManager uiManager = new();
    private GameManager gameManager = new();
    private BallSkillState ballSkillManager = new();
    private MultiGameManager multiGameManager = new();
    private PlayerManager playerManager = new();

    #endregion

    #region Properties

    public static ResourceManager Resource => Instance != null ? Instance.resourceManager : null;
    public static EventManager Event => Instance != null ? Instance.eventManager : null;
    public static UIManager UI => Instance != null ? Instance.uiManager : null;
    public static GameManager Game => Instance != null ? Instance.gameManager : null;
    public static BallSkillState Skill => Instance != null ? Instance.ballSkillManager : null;
    public static MultiGameManager Multi => Instance != null ? Instance.multiGameManager : null;
    public static PlayerManager Player => Instance != null ? Instance.playerManager : null;

    #endregion
}
