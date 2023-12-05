using UnityEngine;
using DG.Tweening;

public class Wall : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            SFX.Instance.PlayOneShot(SFX.Instance.brickHit);

            sprite.DOFade(1, 0.15f);
            sprite.DOFade(20f / 255f, 0.15f).SetDelay(0.15f);
        }
    }
}
