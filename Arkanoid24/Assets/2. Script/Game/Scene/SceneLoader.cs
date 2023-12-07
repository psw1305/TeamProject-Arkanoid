using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneLoader : SingletonBehaviour<SceneLoader>
{
    [SerializeField] private CanvasGroup canvas;
    [SerializeField] private float fadeDuration = 0.5f;

    public void ChangeScene(string sceneName)
    {
        canvas
            .DOFade(1, fadeDuration)
            .OnStart(() => 
            {
                Managers.Game.State = GameState.Loading;
                canvas.blocksRaycasts = true;
            })
            .OnComplete(() => 
            {
                SceneManager.LoadScene(sceneName);
            });
    }

    public void OnSceneLoaded()
    {
        //if (Managers.Game.State != GameState.Loading) return;

        canvas
            .DOFade(0, fadeDuration)
            .OnComplete(() => 
            {
                Managers.Game.State = GameState.Play;
                canvas.blocksRaycasts = false;
            });
    }
}
