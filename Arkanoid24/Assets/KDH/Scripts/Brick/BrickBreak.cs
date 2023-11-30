using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBreak : MonoBehaviour
{
    [SerializeField] int _hp;

    [Range(0f, 100f)]
    [SerializeField] int _itemCreateRate;

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
    private void BrickDestroy()
    {
        instantiateItem();
        Destroy(gameObject);
    }

    //아이템 생성 로직
    private void instantiateItem()
    {
        //_itemCreateRate 이하일 경우 아이템 생성
        if (Random.Range(0, 101) <= _itemCreateRate)
        {
            //원본 알카노이드 아이템이 7개니까 enum을 고려해 0 ~ 7 사이
            Debug.Log(Random.Range(0, 8));
        }
    }
}
