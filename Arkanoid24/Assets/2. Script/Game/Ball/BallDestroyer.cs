using System.Collections;
using UnityEngine;

public class BallDestroyer : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ball"))
        {
            StartCoroutine(DelaySpawn(col));
        }

        if (col.CompareTag("Item"))
        {
            Destroy(col.gameObject);
        }
    }

    private IEnumerator DelaySpawn(Collider2D col)
    {
        SFX.Instance.PlayOneShot(SFX.Instance.ballDeath);
        yield return new WaitForSeconds(0.19f);
        particle.Play();
        yield return new WaitForSeconds(0.06f);
        Managers.Game.LifeDown(col.gameObject);
        Destroy(col.gameObject);
    }
}
