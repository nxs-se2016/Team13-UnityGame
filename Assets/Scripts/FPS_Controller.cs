using UnityEngine;

// RequireComponent ensures that the object has a CharacterController component attached
[RequireComponent(typeof(CharacterController))]
public class FPS_Controller : MonoBehaviour
{
    // Public variables that can be modified in the Unity Inspector

    // The Camera that the player will use to look around
    public Camera playerCamera;

    // Walking and running speeds in units per second
    public float walkSpeed = 6f;
    public float runSpeed = 12f;

    // The force applied when the player jumps
    public float jumpPower = 7f;

    // Gravity applied to the player, pulling them down
    public float gravity = 10f;

    // Look speed controls how fast the player can look around with the mouse
    public float lookSpeed = 100f;

    // Limit for how far up and down the player can look (in degrees)
    public float lookXLimit = 45f;

    // Internal variables for movement and camera rotation
    Vector3 moveDirection = Vector3.zero;  // Holds the player's movement direction
    float rotationX = 0;  // Tracks the up-down rotation of the camera

    // Can be toggled to enable or disable movement (useful for pausing the game)
    public bool canMove = true;

    // Reference to the CharacterController component on the player
    CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        // Get the CharacterController component attached to this object
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Only handle movement and rotation if the game is not paused
        if (GameManager.isGameStarted && !GameManager.isPaused)
        {
            #region Handles Movement
            // Get the direction vectors relative to the player’s orientation
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            // Check if the player is holding the "run" key (Left Shift)
            bool isRunning = Input.GetKey(KeyCode.LeftShift);

            // Calculate movement speed for forward/backward (X) and side-to-side (Y) directions
            float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;

            // Store the Y-axis (up-down) movement before resetting moveDirection (for jumping)
            float movementDirectionY = moveDirection.y;

            // Calculate the movement direction based on player input
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);
            #endregion

            #region Handles Jumping
            // Check if the player is pressing the Jump button and is grounded
            if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
            {
                // Apply jump force in the Y-axis
                moveDirection.y = jumpPower;
            }
            else
            {
                // Retain the current Y-axis velocity (falling or stationary)
                moveDirection.y = movementDirectionY;
            }

            // If the player is not grounded, apply gravity
            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;  // Subtract gravity over time
            }
            #endregion

            #region Handles Rotation
            // Move the player character according to the calculated moveDirection
            characterController.Move(moveDirection * Time.deltaTime);

            // Handle camera and character rotation based on mouse movement
            if (canMove)
            {
                // Up and down rotation (pitch) of the camera based on mouse Y-axis movement
                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed * Time.deltaTime;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);  // Clamp to prevent excessive rotation
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);  // Apply the rotation

                // Left and right rotation (yaw) of the player based on mouse X-axis movement
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed * Time.deltaTime, 0);
            }
            #endregion
        }
    }

    // Method to set the look speed
    public void SetLookSpeed(float newLookSpeed)
    {
        lookSpeed = newLookSpeed;
    }
}

