using System.Collections;
using UnityEngine;

 public class Item : MonoBehaviour
{
    private float _dropSpeed = 3f;
    private Rigidbody2D _rb;
    public float GetSpeed => (_dropSpeed);

    [SerializeField] private Items itemType;
    [SerializeField] private GameObject sprite;
    [SerializeField] private ParticleSystem particle;

    private ModelState state = ModelState.Live;
    //private Ball _mainBall;
    //private float _originSpeed;
    //private GameObject _firstBall;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (transform.position.y < -6) Destroy(gameObject);
        if (Managers.Game.State != GameState.Play)
        {
            _rb.velocity = Vector3.zero;
            return;
        }

        _rb.velocity = _dropSpeed * Vector3.down;
        //transform.position += new Vector3(0, -_dropSpeed, 0) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        var activePlayers = Managers.Player.GetActivePlayers();

        foreach(var player in activePlayers)
        {
            if (player == collision.gameObject && state != ModelState.Dead)
            {
                BallSkillState playerSkill = collision.GetComponent<BallSkillState>();
                playerSkill.Player = collision.gameObject;
                state = ModelState.Dead;
                SFX.Instance.PlayOneShot(SFX.Instance.itemPickup);
                ItemSkill(collision.gameObject, playerSkill);
                StartCoroutine(DeathCoroutine());
            }
        }
    }

    private IEnumerator DeathCoroutine()
    {
        sprite.SetActive(false);
        particle.Play();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void ItemSkill(GameObject player, BallSkillState playerSkill)
    {
        switch (itemType)
        {
            case Items.Player:
                // 목숨 추가
                playerSkill.PlayerItem();
                break;

            case Items.Lasers:
                // 2발씩 발사
                playerSkill.Lasers(player);
                break;

            case Items.Enlarge:
                // 패들이 1.5배 커짐(가로)
                playerSkill.Enalarge(player);
                break;

            case Items.Catch:
                // 공이 튕기지않고 패들에 달라붙음
                playerSkill.Catch();
                break;

            case Items.Slow:
                // 공 속도 감소
                playerSkill.Slow(player);
                break;

            case Items.Disruption:
                // 공 2개 추가
                playerSkill.Disruption(player);
                break;

            case Items.Power:
                playerSkill.PowerUp(player);
            break;

        }
    }

}