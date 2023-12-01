using System.Collections;
using UnityEngine;

public class ArkanoidBallDestroyer : MonoBehaviour
{
    [SerializeField] ArkanoidGame game;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ball"))
        {
            StartCoroutine(DelaySpawn(col));
        }        
    }

    private IEnumerator DelaySpawn(Collider2D col)
    {
        yield return new WaitForSeconds(0.25f);
        game.FireBall();
        Destroy(col.gameObject);
    }
}
