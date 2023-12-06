using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

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

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "TEST_Versus")
        {
            itemCreateRate = 0;
        }
    }

    /// <summary>
    /// �긯 ������ ��� �޼ҵ�
    /// </summary>
    /// <param name="damaged">������ ��ġ</param>
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
    /// �긯 �ı� �޼ҵ�
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

    // ������ ���� ����
    public void InstantiateItem()
    {
        // itemCreateRate ������ ��� ������ ����
        if (Random.Range(0, 101) <= itemCreateRate)
        {
            itemSpawner.transform.position = transform.position;
            Instantiate(itemSpawner);
            itemSpawner.transform.position = Vector2.zero;
        }
    }

    // �浹�� �߻��ϸ� ����
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") || collision.gameObject.CompareTag("Ball1") || collision.gameObject.CompareTag("Ball2"))
        {
            SFX.Instance.PlayOneShot(SFX.Instance.brickHit);
            Damaged(collision.gameObject.GetComponent<Ball>()._maxPower);
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
