using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBreak : MonoBehaviour
{
    [SerializeField] int _hp;

    [Range(0f, 100f)]
    [SerializeField] int _itemCreateRate;

    public GameObject _itemSpawner;
    //충돌이 발생하면 실행
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            _hp--;
            Debug.Log($"현재 체력 {_hp}");
        }
        if(_hp <= 0)
        {
            BrickDestroy();
        }
    }

    //브릭을 삭제하는 메서드
    public void BrickDestroy()
    {
        // [박상원] 벽돌 파괴시 점수 100점 추가
        // 점수는 추후 벽돌 종류에 따라 변경 가능
        Managers.Game.AddScore(100);

        instantiateItem();
        Destroy(gameObject);
    }

    //아이템 생성 로직
    private void instantiateItem()
    {
        //_itemCreateRate 이하일 경우 아이템 생성
        if (Random.Range(0, 101) <= _itemCreateRate)
        {
            _itemSpawner.transform.position = transform.position;
            Instantiate(_itemSpawner);
            _itemSpawner.transform.position = Vector2.zero;
        }
    }
}
