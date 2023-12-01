using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class InstantItem : MonoBehaviour
{
    List<GameObject> _itemList = new List<GameObject>();

    [Header("items")]
    public GameObject _item1;
    public GameObject _item2;
    public GameObject _item3;
    public GameObject _item4;
    public GameObject _item5;
    public GameObject _item6;

    void Start()
    {
        AddItemToList();
        CreatItem();
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //리스트에 아이템들을 담음
    private void AddItemToList()
    {
        _itemList.Add(_item1);
        _itemList.Add(_item2);
        _itemList.Add(_item3);
        _itemList.Add(_item4);
        _itemList.Add(_item5);
        _itemList.Add(_item6);
    }

    private void CreatItem()
    {
        GameObject spawnItem = Instantiate(_itemList[Random.Range(0, _itemList.Count)]);
        spawnItem.transform.position = transform.position;
    }
}
