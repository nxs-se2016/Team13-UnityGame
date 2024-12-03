using UnityEngine;
using UnityEngine.Events;

public class SinglePressurePlate : MonoBehaviour
{
    [SerializeField] private Animator myAnimator;
    [SerializeField] private AudioSource source;
    public UnityEvent onPlatePressed;

    private bool plateTriggered = false; // Tracks if the plate is pressed
    public float resetDelay = 0.5f; // Time before the plate resets itself
    public bool resetAfterUse = false; // Determines if the plate should reset automatically after each use

    private void Start()
    {
        if (myAnimator == null)
            myAnimator = GetComponent<Animator>();

        if (source == null)
            source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !plateTriggered)
        {
            plateTriggered = true; // Mark as pressed
            myAnimator.SetBool("isPressed", true); // Activate pressed animation
            onPlatePressed?.Invoke(); // Trigger event
            source?.Play(); // Play sound

            // Automatically reset the plate after a delay if it needs to be reused
            if (resetAfterUse)
            {
                Invoke(nameof(ResetPlate), resetDelay);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !resetAfterUse)
        {
            plateTriggered = false; // Reset for plates not requiring automatic reset
        }
    }

    // Method to reset the plate for incorrect sequence or reuse
    public void ResetPlate()
    {
        plateTriggered = false; // Allow plate to be pressed again
        myAnimator.SetBool("isPressed", false); // Reset animation
        Debug.Log($"{gameObject.name} has been reset.");
    }
}
