public class Managers : SingletonBehaviour<Managers>
{
    private ResourceManager resource = new();
    private ArkanoidGame game = new();
    private UIManager ui = new();


    public static ResourceManager Resource => Instance != null ? Instance.resource : null;
    public static ArkanoidGame Game => Instance != null ? Instance.game : null;
    public static UIManager UI => Instance != null ? Instance.ui : null;

    protected override void Awake()
    {
        base.Awake();

        Resource.Initialize();
    }
}
