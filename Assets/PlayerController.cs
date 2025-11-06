using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public Camera main;

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector3 velocity;

    public int CanUpdateCount = 0;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        if (CanUpdateCount > 0)
        {
            CanUpdateCount--;
        }
        else
        {
            // --- Mouvement relatif à la caméra ---
            Vector3 camForward = main.transform.forward;
            camForward.y = 0f;
            Vector3 camRight = main.transform.right;
            camRight.y = 0f;

            Vector3 move = camForward * moveInput.y + camRight * moveInput.x;
            if (move.magnitude > 1f) move.Normalize();

            controller.Move(move * moveSpeed * Time.deltaTime);

            // --- Gravité & saut ---
            if (controller.isGrounded && velocity.y < 0)
            {
                velocity.y = -2f; // Empêche l'accumulation de gravité au sol
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    // --- Appelé par l'Input System ---
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // --- Saut ---
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}


