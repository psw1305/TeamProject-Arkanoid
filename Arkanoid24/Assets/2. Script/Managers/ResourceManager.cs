using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    private Dictionary<string, GameObject> prefabs = new();

    public void Initialize()
    {
        // ������Ʈ ������
        GameObject[] objs = Resources.LoadAll<GameObject>("Prefabs/Model");
        foreach (GameObject obj in objs) prefabs[obj.name] = obj;
    }

    /// <summary>
    /// ���ҽ� ���� ���� ������ ����
    /// </summary>
    /// <param name="prefabName">�ش� ������ �̸�</param>
    /// <returns>�ش� ������ ����</returns>
    public GameObject Instantiate(string prefabName, Vector2 startPos)
    {
        if (!prefabs.TryGetValue(prefabName, out GameObject prefab)) return null;
        return GameObject.Instantiate(prefab, startPos, Quaternion.identity);
    }
}
