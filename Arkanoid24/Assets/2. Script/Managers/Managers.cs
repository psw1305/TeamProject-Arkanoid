public class Managers : SingletonBehaviour<Managers>
{
    #region Managers Variables
    private ResourceManager resourceManager = new();
    private EventManager eventManager = new();

    private ArkanoidGame gameManager = new();
    #endregion



    #region Properties
    public static ResourceManager Resource => Instance != null ? Instance.resourceManager : null;
    public static EventManager Event => Instance != null ? Instance.eventManager : null;
    public static ArkanoidGame Game => Instance != null ? Instance.gameManager : null;
    #endregion



    protected override void Awake()
    {
        base.Awake();

        Resource.Initialize();
    }
}
