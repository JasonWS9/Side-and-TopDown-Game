using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    
    public static PlayerMovement Instance;

    private CharacterController characterController;

    private float moveX;
    private float moveY;

    private InputAction moveAction;

    [SerializeField] private float moveSpeed;
    void Awake()
    {
        Instance = this;
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }


    void Update()
    {
        GetInput();
        HandleMovement();
        HandleJump();
    }
    private void GetInput()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();

        moveX = moveValue.x * moveSpeed * Time.deltaTime;
        moveY = moveValue.y * moveSpeed * Time.deltaTime;


    }
    private void HandleMovement()
    {
        Vector3 finalMove = new Vector3();

        if (GameManager.Instance.currentState == GameManager.PerspectiveState.TopDown)
        {
            finalMove = new Vector3 (moveX, 0, moveY);
        } else 
        {
            finalMove = new Vector3(0, 0, moveX);
        }
        characterController.Move(finalMove);
    }

    private void HandleJump()
    {

    }

}
