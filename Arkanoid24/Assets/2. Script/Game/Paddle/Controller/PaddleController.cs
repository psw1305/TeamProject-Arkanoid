
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public enum PADDLE_STATE
    {
        READY,
        MOVEMENT
    }

    public PADDLE_STATE PaddleState {  get; set; } = PADDLE_STATE.READY;

    private void Start()
    {
        ServiceLocator.RegisterService(this);
    }
}