using System.Buffers;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    
    public static PlayerMovement Instance;
    private CharacterController characterController;

    private InputAction moveAction;
    private InputAction jumpAction;

    private float moveX;
    private float moveY;
    [HideInInspector] public bool movementEnabled = true;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravity = -20f;

    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    private bool isGrounded;
    private Vector3 velocity;

    void Awake()
    {
        Instance = this;
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        jumpAction = InputSystem.actions.FindAction("Jump");
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        if (movementEnabled)
        {
            GetInput();
            HandleMovement();
            HandleJump();
        }
    }
    private void GetInput()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        moveX = moveValue.x;
        moveY = moveValue.y;
    }
    private void HandleMovement()
    {
        Vector3 finalMove = Vector3.zero;

        if (PerspectiveManager.Instance.currentState == PerspectiveManager.PerspectiveState.TopDown)
        {
            finalMove = new Vector3 (moveX, 0f, moveY);
        } else 
        {
            finalMove = new Vector3(0, 0f, moveX);
        }
        characterController.Move(finalMove * moveSpeed * Time.deltaTime);
    }
    private void HandleJump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }
        if (PerspectiveManager.Instance.currentState == PerspectiveManager.PerspectiveState.Side)
        {
            if (jumpAction.WasPressedThisFrame() && isGrounded)
            {
                Debug.Log("should jump");
                velocity.y = jumpForce;
            }
        }
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
