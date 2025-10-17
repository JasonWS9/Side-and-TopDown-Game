using Unity.VisualScripting;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector] public bool canShiftPerspective;

    public static event Action OnPerspectiveShift;

    public CameraState currentState = CameraState.TopDown;
    public enum CameraState
    {
        TopDown,
        Side
    }

    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ShiftPerspective();
        }
    }

    public void ShiftPerspective()
    {
        if (currentState == CameraState.TopDown)
        {
            currentState = CameraState.Side;
        } else if (currentState == CameraState.Side)
        {
            currentState = CameraState.TopDown;
        }

        OnPerspectiveShift?.Invoke();
    }
}
