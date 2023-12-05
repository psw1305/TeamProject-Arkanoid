using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private int power = 1;
    [SerializeField] private GameObject sprite;
    [SerializeField] private ParticleSystem particle;

    public int Power => power;

    private ModelState state = ModelState.Live;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (state == ModelState.Dead || Managers.Game.State == GameState.Pause)
        {
            _rb.velocity = Vector3.zero;
            return;
        }

        _rb.velocity = speed * Vector3.up;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (state == ModelState.Dead) return;

        if (collider.CompareTag("Brick") || collider.CompareTag("Wall"))
        {
            SFX.Instance.PlayOneShot(SFX.Instance.brickHit);
            state = ModelState.Dead;
            StartCoroutine(DeathCoroutine());
        }
    }

    public IEnumerator DeathCoroutine()
    {
        sprite.SetActive(false);
        particle.Play();
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
