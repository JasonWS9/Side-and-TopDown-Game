using Unity.VisualScripting;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector] public bool canShiftPerspective;

    public static event Action OnPerspectiveShift;

    public PerspectiveState currentState = PerspectiveState.TopDown;
    public enum PerspectiveState
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

    }

    public void ShiftPerspective()
    {
        if (currentState == PerspectiveState.TopDown)
        {
            currentState = PerspectiveState.Side;
        } else if (currentState == PerspectiveState.Side)
        {
            currentState = PerspectiveState.TopDown;
        }

        OnPerspectiveShift?.Invoke();
    }
}
