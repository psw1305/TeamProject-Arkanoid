public class Managers : SingletonBehaviour<Managers>
{
    private ResourceManager resource = new();
    private ArkanoidGame game = new();
    
    public static ResourceManager Resource => Instance != null ? Instance.resource : null;
    public static ArkanoidGame Game => Instance != null ? Instance.game : null;

    protected override void Awake()
    {
        base.Awake();

        Resource.Initialize();
    }
}
