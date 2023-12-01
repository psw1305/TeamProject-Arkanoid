using System.Collections;
using UnityEngine;

public class BallDestroyer : MonoBehaviour
{
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
        Managers.Game.CurrentBalls.Remove(col.gameObject);
        Managers.Game.InstanceBall();
        Destroy(col.gameObject);
    }
}
