using UnityEngine;
using UnityEngine.Events;

public class SinglePressurePlate : MonoBehaviour
{
    [SerializeField] private Animator myAnimator;
    [SerializeField] private AudioSource source;
    public UnityEvent onPlatePressed;

    private bool plateTriggered = false;

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
            plateTriggered = true;
            myAnimator.SetBool("isPressed", true); // Activate the pressed state
            onPlatePressed?.Invoke(); // Trigger any events
            source?.Play(); // Play audio if available
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myAnimator.SetBool("isPressed", false); // Reset isPressed when player leaves
        }
    }

    // Method to reset the plate to its initial state
    public void ResetPlate()
    {
        myAnimator.SetTrigger("Reset"); // Trigger the Reset animation
        plateTriggered = false; // Allow the plate to be triggered again
    }
}
