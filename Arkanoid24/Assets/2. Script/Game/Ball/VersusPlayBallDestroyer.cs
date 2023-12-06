using System.Collections;
using UnityEngine;

public class VersusPlayBallDestroyer : MonoBehaviour
{
    [SerializeField] private ParticleSystem player1particle;
    [SerializeField] private ParticleSystem player2particle;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ball1") || col.CompareTag("Ball2"))
        {
            StartCoroutine(DelaySpawn(col));
        }        
    }

    private IEnumerator DelaySpawn(Collider2D col)
    {
        if (col.CompareTag("Ball1"))
        {
            SFX.Instance.PlayOneShot(SFX.Instance.ballDeath);
            yield return new WaitForSeconds(0.19f);
            player1particle.Play();
            yield return new WaitForSeconds(0.06f);
            Destroy(col.gameObject);
            Managers.Versus.VersusUI.ShowGameOver("Player2");
        }
        else if (col.CompareTag("Ball2"))
        {
            SFX.Instance.PlayOneShot(SFX.Instance.ballDeath);
            yield return new WaitForSeconds(0.19f);
            player2particle.Play();
            yield return new WaitForSeconds(0.06f);
            Destroy(col.gameObject);
            Managers.Versus.VersusUI.ShowGameOver("Player1");
        }

    }
}
