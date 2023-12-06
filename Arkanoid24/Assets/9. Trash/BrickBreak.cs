using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrickBreak : MonoBehaviour
{
    public int _hp;

    [Range(0f, 100f)]
    public int _itemCreateRate;

    public GameObject _itemSpawner;

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "VersusMode")
        {
            _itemCreateRate = 0;
        }
    }
    //충돌이 발생하면 실행
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            _hp -= collision.gameObject.GetComponent<Ball>()._maxPower;
        }
        //레이저 충돌
        else if (collision.gameObject.tag == "Bullet")
        {
            //_hp -= collision.gameObject.GetComponent<Laser>()._maxPower;
        }
        if (_hp <= 0)
        {
            BrickDestroy(collision.gameObject.tag);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter");
        if (collision.gameObject.tag == "Bullet")
        {
            _hp -= collision.gameObject.GetComponent<Laser>().Power;
        }
        if (_hp <= 0)
        {
            Debug.Log("HP 0");
            BrickDestroy(collision.gameObject.tag);
        }
    }

    //브릭을 삭제하는 메서드
    public void BrickDestroy(string ballType)
    {
        var players = Managers.Player.GetActivePlayers();

        if (ballType == "Ball" || ballType == "Bullet") 
        {
            // [박상원] 벽돌 파괴시 점수 100점 추가
            // 점수는 추후 벽돌 종류에 따라 변경 가능
            Managers.Game.AddScore(100);
            Debug.Log("Ball Destory");

            foreach(var player in players)
            {
                if (player.tag == "Player1")
                    Managers.Versus.Player1BrickCount();
                else if (player.tag == "Player2")
                    Managers.Versus.Player2BrickCount();
            }
        }
        
        // MultiPlayer

        //else if(ballType == "Ball1")
        //{
        //    Managers.Versus.Player1BrickCount();
        //}
        //else if(ballType == "Ball2")
        //{
        //    Managers.Versus.Player2BrickCount();
        //}

        instantiateItem();
        Destroy(gameObject);
    }

    //아이템 생성 로직
    public void instantiateItem()
    {
        //_itemCreateRate 이하일 경우 아이템 생성
        if (Random.Range(0, 101) < _itemCreateRate)
        {
            _itemSpawner.transform.position = transform.position;
            Instantiate(_itemSpawner);
            _itemSpawner.transform.position = Vector2.zero;
        }
    }
}
