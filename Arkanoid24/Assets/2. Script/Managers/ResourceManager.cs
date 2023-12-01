using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    private Dictionary<string, GameObject> stages = new();
    private Dictionary<string, GameObject> prefabs = new();

    public void Initialize()
    {
        // 스테이지 SO 파일
        GameObject[] stages = Resources.LoadAll<GameObject>("Bluepritn/Stage");
        foreach (GameObject stage in stages) this.stages[stage.name] = stage;

        // 오브젝트 프리팹
        GameObject[] objs = Resources.LoadAll<GameObject>("Prefabs/Model");
        foreach (GameObject obj in objs) prefabs[obj.name] = obj;
    }

    /// <summary>
    /// 리소스 폴더 내에 프리팹 생성
    /// </summary>
    /// <param name="prefabName">해당 프리팹 이름</param>
    /// <returns>해당 프리팹 생성</returns>
    public GameObject Instantiate(string prefabName, Vector2 startPos)
    {
        if (!prefabs.TryGetValue(prefabName, out GameObject prefab)) return null;
        return GameObject.Instantiate(prefab, startPos, Quaternion.identity);
    }
}

