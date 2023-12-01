
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    private Rigidbody2D _rbody;

    private Vector2 _movementAxis;
    private float _movementSpeed = 10f;

    private void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();

        Managers.Event.OnMoveEvent += Movement;
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void Movement(Vector2 axis)
    {
        _movementAxis = axis;
    }

    private void ApplyMovement()
    {
        float distancePaddleToMove = _movementAxis.x - transform.position.x;

        float moveSpeed = distancePaddleToMove * _movementSpeed;

        _rbody.velocity = new Vector2(moveSpeed, 0);
    }
}
