using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PerspectiveManager : MonoBehaviour
{
    public static PerspectiveManager Instance;

    public static event Action OnPerspectiveShift;

    [HideInInspector] public PerspectiveState currentState = PerspectiveState.TopDown;

    [HideInInspector] public bool canChangePerspective = true;
    [HideInInspector] public enum PerspectiveState
    {
        TopDown,
        Side
    }

    private InputAction swapAction;

    private void Start()
    {
        swapAction = InputSystem.actions.FindAction("Switch");
    }
    void Update()
    {
        if (swapAction.WasPressedThisFrame())
        {
            ShiftPerspective();
        }
    }
    void Awake()
    {
        Instance = this;
    }
    public void ShiftPerspective()
    {
        if (canChangePerspective)
        {
            if (currentState == PerspectiveState.TopDown)
            {
                currentState = PerspectiveState.Side;
            }
            else if (currentState == PerspectiveState.Side)
            {
                currentState = PerspectiveState.TopDown;
            }

            OnPerspectiveShift?.Invoke();
        }
    }
}
