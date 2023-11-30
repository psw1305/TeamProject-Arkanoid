using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        None,
        Laswer,
        Enlarge,
        Catch,
        Slow,
    }
    public ItemType itemType = ItemType.None;

    [SerializeField] float _itemSpeed = 5f;
    [SerializeField] GameObject _paddle;


    void Update()
    {
        transform.position += new Vector3(0, -_itemSpeed , 0) * Time.deltaTime;
    }

    public void LaserItem()
    {

    }

    public void EnlargeItem()
    {
        _paddle.transform.localScale += new Vector3(0.5f, 0, 0);
    }

    public void CatchItem()
    {
        // ball이 튕겨 나가지 않고, 달라 붙음
        // 클릭 시, ball 발사
    }

    public void SlowItem()
    {
        // ball 속도가 느려짐
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            switch (itemType)
            {
                case ItemType.None:
                    break;

                case ItemType.Laswer:
                    LaserItem();
                    break;

                case ItemType.Enlarge:
                    EnlargeItem();
                    break;

                case ItemType.Catch:
                    CatchItem();
                    break;

                case ItemType.Slow:
                    SlowItem();
                    break;

                default:
                    break;
            }


            Destroy(gameObject);
        }
    }
}
