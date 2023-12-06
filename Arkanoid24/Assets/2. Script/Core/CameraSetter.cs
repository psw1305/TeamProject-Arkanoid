
using UnityEngine;

public class CameraSetter : MonoBehaviour
{
    private Canvas _mainUI;

    private void Awake()
    {
        _mainUI = FindObjectOfType<Canvas>();
    }

    private void Start()
    {
        InitCanvasCamera();
    }

    private void InitCanvasCamera()
    {
        if (_mainUI == null) throw new System.Exception("Canvas가 존재하지 않습니다.");

        _mainUI.worldCamera = gameObject.GetComponent<Camera>();
    }
}
