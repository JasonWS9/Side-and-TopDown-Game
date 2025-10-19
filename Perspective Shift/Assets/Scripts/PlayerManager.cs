using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{


    private InputAction swapAction;

    private void Start()
    {
        swapAction = InputSystem.actions.FindAction("Switch");
    }
    void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (swapAction.WasPressedThisFrame())
        {
            GameManager.Instance.ShiftPerspective();
        }
    }

}
