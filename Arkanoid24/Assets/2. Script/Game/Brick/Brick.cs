using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Brick : MonoBehaviour
{
    [SerializeField] private int hp;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private ParticleSystem particle;

    [Header("Item")]
    [SerializeField][Range(0f, 100f)]
    private int itemCreateRate;
    [SerializeField] 
    private GameObject itemSpawner;

    /// <summary>
    /// 브릭 데미지 계산 메소드
    /// </summary>
    /// <param name="damaged">데미지 수치</param>
    public void Damaged(int damaged)
    {
        hp -= damaged;

        if (hp <= 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(DeathCoroutine());
        }
        else
        {
            sprite.DOFade(1, 0.15f);
            sprite.DOFade(80f / 255f, 0.15f).SetDelay(0.15f);
        }
    }

    /// <summary>
    /// 브릭 파괴 메소드
    /// </summary>
    /// <returns></returns>
    public IEnumerator DeathCoroutine()
    {
        sprite.gameObject.SetActive(false);
        particle.Play();
        Managers.Game.AddScore(100);
        InstantiateItem();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    // 아이템 생성 로직
    public void InstantiateItem()
    {
        // itemCreateRate 이하일 경우 아이템 생성
        if (Random.Range(0, 101) <= itemCreateRate)
        {
            itemSpawner.transform.position = transform.position;
            Instantiate(itemSpawner);
            itemSpawner.transform.position = Vector2.zero;
        }
    }

    // 충돌이 발생하면 실행
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            SFX.Instance.PlayOneShot(SFX.Instance.brickHit);
            Damaged(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Bullet"))
        {
            Damaged(collider.GetComponent<Laser>().Power);
        }
    }
}
