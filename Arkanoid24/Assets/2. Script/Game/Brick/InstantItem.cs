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
        int excludeIndex = Random.Range(0, itemList.Count);

        if (Managers.Game.Mode == GameMode.Versus)
        {
            while(excludeIndex == 4 || excludeIndex == 3)
            {
                excludeIndex = Random.Range(0, itemList.Count);
            }
        }
        
        GameObject spawnItem = Instantiate(itemList[excludeIndex]);
        spawnItem.transform.position = transform.position;
    }
}
