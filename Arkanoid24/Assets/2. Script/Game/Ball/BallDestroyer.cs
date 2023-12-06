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

        // 볼 삭제처리
        GameObject ball = col.gameObject;
        GameObject playerOwner = ball.GetComponent<BallPreference>().BallOwner;
        Managers.Ball.RemoveBallFromPlayer(playerOwner, ball);

        Managers.Game.LifeDown(playerOwner);

        Destroy(col.gameObject);
    }
}