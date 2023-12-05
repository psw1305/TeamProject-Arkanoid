
using System;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    #region Member Variables

    private PaddleController _paddleController;

    private Rigidbody2D _rbody;

    private Vector2 _movementAxis;
    private float _movementSpeed = 10f;

    private bool _isMultiplay;

    #endregion



    #region Unity Flow
    private void Start()
    {
        _paddleController = GetComponent<PaddleController>();
        _rbody = GetComponent<Rigidbody2D>();

        _isMultiplay = Managers.Game.IsMulti;

        _paddleController.OnMoveEvent += Movement;
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }
    #endregion



    #region Event
    private void Movement(Vector2 axis)
    {
        _movementAxis = axis;
    }
    #endregion



    #region Apply Methods
    private void ApplyMovement()
    {
        if (_isMultiplay)
            KeyboardMovement();
        else
            MouseMovement();
    }

    private void MouseMovement()
    {
        float distancePaddleToMove = _movementAxis.x - transform.position.x;

        float axisXVelocity = distancePaddleToMove * _movementSpeed;

        _rbody.velocity = new Vector2(axisXVelocity, 0);
    }

    private void KeyboardMovement()
    {
        float axisXVelocity = _movementAxis.x * _movementSpeed;

        _rbody.velocity = new Vector2(axisXVelocity, 0);
    }
    #endregion



    private void OnDisable()
    {
        _paddleController.OnMoveEvent -= Movement;
    }
}