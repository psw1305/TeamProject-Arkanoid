using UnityEngine;

public class LobbyScene : MonoBehaviour
{
    void Start()
    {
        SceneLoader.Instance.OnSceneLoaded();
    }
}
