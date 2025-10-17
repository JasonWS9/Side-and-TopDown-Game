using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    public static PlayerController Instance;

    private CharacterController characterController;

    private bool isTopDown = true;

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
        ShiftControls();
    }
    private void OnEnable()
    {
        GameManager.OnPerspectiveShift += ShiftControls;
    }
    private void OnDisable()
    {
        GameManager.OnPerspectiveShift -= ShiftControls;
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

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        moveX = moveValue.x * moveSpeed * Time.deltaTime;
        moveY = moveValue.y * moveSpeed * Time.deltaTime;


    }

    private void OnMove()
    {
        
    }
    private void HandleMovement()
    {
        Vector3 finalMove = new Vector3();

        if (isTopDown)
        {
            finalMove = new Vector3 (moveX, 0, moveY);
            characterController.Move(finalMove);
        } else
        {
            finalMove = new Vector3(moveX, 0, 0);
        }
    }

    private void HandleJump()
    {

    }
    private void ShiftControls()
    {
        if (GameManager.Instance.currentState == GameManager.CameraState.TopDown)
        {
            isTopDown = true;
        } else { isTopDown = false; }
    }
}
