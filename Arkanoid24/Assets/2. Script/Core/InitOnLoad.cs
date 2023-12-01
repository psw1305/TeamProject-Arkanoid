using UnityEngine;

/// <summary>
/// 씬 로드하는 동안 파괴하지 않을 오브젝트 폴더 불러오기
/// </summary>
internal static class InitOnLoad
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitApplication()
    {
        GameObject[] prefabs = Resources.LoadAll<GameObject>("InitOnLoad");

        if (prefabs.Length > 0)
        {
            foreach (var prefab in prefabs)
            {
                GameObject go = Object.Instantiate(prefab);
                go.name = prefab.name;
                Object.DontDestroyOnLoad(go);
            }
        }
    }
}