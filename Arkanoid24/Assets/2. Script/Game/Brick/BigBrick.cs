using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BigBrick : BrickBreak
{

    public Sprite _phaseHealthSprite1;
    public Sprite _phaseHealthSprite2;
    public Sprite _phaseHealthSprite3;
    public Sprite _phaseHealthSprite4;
    public Sprite _phaseHealthSprite5;

    //충돌이 발생하면 실행
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            _hp--;
        }
        else if (collision.gameObject.tag == "Bullet")
        {
            _hp -= collision.gameObject.GetComponent<Laser>()._power;
        }
        PhaseSpriteSetting();
        if (_hp <= 0)
        {
            BrickDestroy();
        }
    }

    private void PhaseSpriteSetting()
    {
        switch (_hp)
        {
            case 1:
                GetComponent<SpriteRenderer>().sprite = _phaseHealthSprite5;
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = _phaseHealthSprite4;
                break;
            case 3:
                GetComponent<SpriteRenderer>().sprite = _phaseHealthSprite3;
                break;
            case 4:
                GetComponent<SpriteRenderer>().sprite = _phaseHealthSprite2;
                break;
            default:
                GetComponent<SpriteRenderer>().sprite = _phaseHealthSprite1;
                break;

        }
    }
}
