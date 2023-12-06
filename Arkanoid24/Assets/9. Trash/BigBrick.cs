using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BigBrick : MonoBehaviour
{
    [SerializeField] private int hp;

    [Range(0f, 100f)]
    public int itemCreateRate;
    public GameObject itemSpawner;

    public Sprite _phaseHealthSprite1;
    public Sprite _phaseHealthSprite2;
    public Sprite _phaseHealthSprite3;
    public Sprite _phaseHealthSprite4;
    public Sprite _phaseHealthSprite5;

    //�浹�� �߻��ϸ� ����
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            hp--;
        }

        PhaseSpriteSetting();
        
        if (hp <= 0)
        {
            BrickDestroy();
        }
    }

    public void BrickDestroy()
    {
        Managers.Game.AddScore(100);

        InstantiateItem();
        Destroy(gameObject);
    }

    // ������ ���� ����
    public void InstantiateItem()
    {
        //_itemCreateRate ������ ��� ������ ����
        if (Random.Range(0, 101) <= itemCreateRate)
        {
            itemSpawner.transform.position = transform.position;
            Instantiate(itemSpawner);
            itemSpawner.transform.position = Vector2.zero;
        }
    }

    private void PhaseSpriteSetting()
    {
        GetComponent<SpriteRenderer>().sprite = hp switch
        {
            1 => _phaseHealthSprite5,
            2 => _phaseHealthSprite4,
            3 => _phaseHealthSprite3,
            4 => _phaseHealthSprite2,
            _ => _phaseHealthSprite1,
        };
    }
}
