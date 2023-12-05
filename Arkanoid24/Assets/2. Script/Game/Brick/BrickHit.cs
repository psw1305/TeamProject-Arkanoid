using UnityEngine;

public class BrickHit : MonoBehaviour
{
    //SpriteRenderer _spriteRenderer;
    //Color _color;

    private void Start()
    {
        //_spriteRenderer = GetComponent<SpriteRenderer>();
        //_color = _spriteRenderer.color;
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            SFX.Instance.PlayOneShot(SFX.Instance.brickHit);
            BrickHitColor();
            Invoke("BrickNormalColor", 0.2f);
        }
    }
    //충돌 시 Brick 투명도를 0.6으로 
    private void BrickHitColor()
    {
        //gameObject.GetComponent<SpriteRenderer>().color = new Color(_color.r, _color.g, _color.b, 0.6f);
    }
    //Brick 투명도를 다시 1로
    private void BrickNormalColor()
    {
        //gameObject.GetComponent<SpriteRenderer>().color = new Color(_color.r, _color.g, _color.b, 1f);
    }
}
