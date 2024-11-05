using UnityEngine;
using UnityEngine.Events;

public class SinglePressurePlate : MonoBehaviour
{
    [SerializeField] private Animator myAnimator;
    [SerializeField] private AudioSource source;
    public UnityEvent onPlatePressed;
    public UnityEvent onPlateReleased;

    private void Start()
    {
        if (myAnimator == null)
            myAnimator = GetComponent<Animator>();

        if (source == null)
            source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myAnimator.SetBool("isPressed", true); // Set isPressed to true
            onPlatePressed?.Invoke();
            source?.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myAnimator.SetBool("isPressed", false); // Set isPressed to false
            onPlateReleased?.Invoke();
        }
    }
}
