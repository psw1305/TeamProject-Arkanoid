using System.Collections.Generic;
using UnityEngine;

public class InstantItem : MonoBehaviour
{
    [Header("items")]
    public List<GameObject> itemList = new List<GameObject>();

    void Start()
    {
        CreatItem();
        Destroy(gameObject);
    }

    private void CreatItem()
    {
        GameObject spawnItem = Instantiate(itemList[Random.Range(0, itemList.Count)]);
        spawnItem.transform.position = transform.position;
    }
}
