using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_test : MonoBehaviour
{
    public enum ItemType
    {
        None,
        Laswer,
        Enlarge,
        Catch,
        Slow,
        Power,
    }

    public ItemType itemType = ItemType.None;

    [SerializeField]float _itemSpeed = 5f;
    [SerializeField]GameObject _paddle;
    [SerializeField]PaddleNJM _paddleNJM;
    [SerializeField] ArkanoidBall _arkanoidBall;



    void OnUpdate()
    {
        transform.position += new Vector3(0, -_itemSpeed , 0) * Time.deltaTime;
    }

    public void LaserItem()
    {
        _paddleNJM.IsLaser = true;
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
        // ball speed가 private로 되어있음
    }

    public void PowerItem()
    {
        // ball 데미지 증가
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
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

                case ItemType.Power:
                    PowerItem();
                    break;

                default:
                    break;
            }

            Destroy(gameObject);
        }
    }
}
