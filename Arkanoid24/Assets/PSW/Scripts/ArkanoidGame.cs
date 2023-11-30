using UnityEngine;

public class ArkanoidGame : MonoBehaviour
{
    private EdgeCollider2D screenEdge;

    [Header("Ball")]
    [SerializeField] private ArkanoidBall ball;

    private void Awake()
    {
        screenEdge = GetComponent<EdgeCollider2D>();
    }

    private void Start()
    {
        screenEdge.GenerateCameraBounds();
        FireBall();
    }

    private void FireBall()
    {
        
    }
}
