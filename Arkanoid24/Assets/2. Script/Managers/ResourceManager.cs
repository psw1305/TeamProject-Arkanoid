using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    private List<StageBlueprint> stages = new();
    private Dictionary<string, GameObject> prefabs = new();

    private List<VersusLevelBlueprint> versusStages = new();
    public void Initialize()
    {
        // 스테이지 SO 파일
        StageBlueprint[] stages = Resources.LoadAll<StageBlueprint>("Blueprint/Stage");
        foreach (StageBlueprint stage in stages) this.stages.Add(stage);

        //대전 스테이지 SO파일
        VersusLevelBlueprint[] versusStages = Resources.LoadAll<VersusLevelBlueprint>("Blueprint/VersusStage");
        foreach (VersusLevelBlueprint vStage in versusStages) this.versusStages.Add(vStage);

        // 오브젝트 프리팹
        GameObject[] objs = Resources.LoadAll<GameObject>("Prefabs/Model");
        foreach (GameObject obj in objs) prefabs[obj.name] = obj;

        GameObject laser = Resources.Load<GameObject>("Prefabs/Laser/Laser");
        prefabs[laser.name] = laser;
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

    public GameObject Instantiate(string prefabName)
    {
        if (!prefabs.TryGetValue(prefabName, out GameObject prefab)) return null;
        return GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
    }

    public StageBlueprint[] GetStages()
    {
        return stages.ToArray();
    }

    public VersusLevelBlueprint[] GetVersusStages()
    {
        return versusStages.ToArray();
    }
}

