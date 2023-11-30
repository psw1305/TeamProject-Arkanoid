
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    private PaddleEventController _controller;
    private Rigidbody2D _rbody;

    private Vector2 _movementAxis;
    private float _movementSpeed = 10f;

    private void Start()
    {
        _controller = GetComponent<PaddleEventController>();
        _rbody = GetComponent<Rigidbody2D>();

        _controller.OnMoveEvent += Movement;
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

        //Vector3 targetPosition = new Vector3(_movementAxis.x, transform.position.y, transform.position.z);

        //transform.position = Vector3.Lerp(transform.position, targetPosition, _movementSpeed * Time.fixedDeltaTime);
    }
}
