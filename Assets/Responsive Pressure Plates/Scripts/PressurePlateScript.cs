using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PressurePlateScript : MonoBehaviour
{
    // Variables for plate detection
    public string firstPlateTag = "FirstPlate";  // Tag for the first plate
    public string otherPlateTag = "OtherPlate";  // Tag for the other two plates
    public float timeLimit = 60f;                // Time limit to step on other plates (60 seconds)

    // Variables to track plate state and timer
    private bool firstPlateTriggered = false;
    private bool secondPlateTriggered = false;
    private bool thirdPlateTriggered = false;
    private float timer = 0f;

    // Serialized fields for editor interaction
    [SerializeField] public string[] options;  // Available tags for interaction (shown in editor)
    [SerializeField] public int index;         // Index to select tag

    // Serialized properties for editor script handling collision events
    public UnityEvent TagCollisionEnter;  // Event triggered on tag collision enter
    public UnityEvent TagCollisionExit;   // Event triggered on tag collision exit
    public UnityEvent TagCollisionStay;   // Event triggered on tag collision stay
    public bool ShowCollisionEnterEvent = true;  // Toggle showing of the enter event in inspector
    public bool ShowCollisionStayEvent = true;   // Toggle showing of the stay event in inspector
    public bool ShowCollisionExitEvent = true;   // Toggle showing of the exit event in inspector
    public bool DisableAudio = false;  // Option to disable audio
    public bool DisableAnimations = false;  // Option to disable animations
    public bool isInteractive = true;  // Should the plate be interactive

    // Reference to Animator and AudioSource components
    private Animator anim;
    private AudioSource source;

    // UnityEvents to trigger specific actions
    public UnityEvent onTimerStart;
    public UnityEvent onTimerFail;
    public UnityEvent onAllPlatesComplete;

    private void Start()
    {
        // Initialize components
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();

        // Check if components are found correctly
        if (anim == null)
            Debug.LogError("Animator component not found on the pressure plate!");
        if (source == null)
            Debug.LogError("AudioSource component not found on the pressure plate!");
    }

    private void OnEnable()
    {
        // Initialize options array with all available tags
        InitializeOptions();
        Debug.Log("PressurePlateScript options initialized with " + options.Length + " tags.");
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        // Ensure tags are initialized in editor mode as well
        InitializeOptions();
    }

    // Force Unity to update options in Edit mode constantly
    private void InitializeOptions()
    {
        options = UnityEditorInternal.InternalEditorUtility.tags;
        if (options.Length > 0 && index >= options.Length)
        {
            index = 0;
        }

        // Ensure updates occur even in Edit mode
        EditorApplication.update -= UpdateInEditor; // Unsubscribe any previous instances
        EditorApplication.update += UpdateInEditor; // Subscribe the update function
    }

    private void UpdateInEditor()
    {
        // Ensure this only happens when not playing and that the object exists
        if (!Application.isPlaying && this != null)
        {
            EditorUtility.SetDirty(this);
        }
        else
        {
            EditorApplication.update -= UpdateInEditor; // Unregister if the object no longer exists
        }
    }
#endif

    private void OnDestroy()
    {
#if UNITY_EDITOR
        EditorApplication.update -= UpdateInEditor; // Unregister when the object is destroyed
#endif
    }

    private void Update()
    {
        if (firstPlateTriggered && timer > 0)
        {
            timer -= Time.deltaTime;

            Debug.Log("Timer is running: " + timer);

            if (timer <= 0 && (!secondPlateTriggered || !thirdPlateTriggered))
            {
                Debug.Log("Time ran out! Resetting the plates.");
                ResetPlates();
                onTimerFail.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ignore collision with the terrain
        if (other.gameObject.CompareTag("Terrain"))
        {
            Debug.Log("Ignoring collision with Terrain");
            return;
        }

        Debug.Log("Collision detected with: " + other.gameObject.name);

        // Check if the collider's tag matches the first plate tag
        if (other.gameObject.tag == firstPlateTag && !firstPlateTriggered)
        {
            Debug.Log("Trigger Entered: Detected first plate!");
            firstPlateTriggered = true;

            // Ensure the Animator and AudioSource are correctly assigned
            if (anim == null)
                anim = GetComponent<Animator>();
            if (source == null)
                source = GetComponent<AudioSource>();

            // Set the "Press" trigger to activate the press animation
            if (anim != null)
            {
                Debug.Log("Setting Press Trigger");
                anim.SetTrigger("Press");
                Debug.Log("Press Trigger Set");
            }
            else
            {
                Debug.LogError("Animator is not assigned or found!");
            }

            if (source != null)
                source.Play();
            else
                Debug.LogError("AudioSource is not assigned or found!");

            Debug.Log("First plate triggered!");
        }

        // Start the timer if it hasn't started yet
        if (other.gameObject.tag == firstPlateTag && firstPlateTriggered && timer == 0)
        {
            timer = timeLimit;
            onTimerStart.Invoke();
            Debug.Log("Timer started!");
        }

        // Check if other plates are being stepped on
        if (other.gameObject.tag == otherPlateTag && firstPlateTriggered)
        {
            if (!secondPlateTriggered)
            {
                secondPlateTriggered = true;
                anim?.SetTrigger("Press"); // Set the trigger for other plates
                source?.Play();
                Debug.Log("Second plate triggered!");
            }
            else if (!thirdPlateTriggered)
            {
                thirdPlateTriggered = true;
                anim?.SetTrigger("Press"); // Set the trigger for the third plate
                source?.Play();
                Debug.Log("Third plate triggered!");

                if (firstPlateTriggered && secondPlateTriggered && thirdPlateTriggered)
                {
                    onAllPlatesComplete.Invoke();
                    Debug.Log("All plates triggered! Success.");
                }
            }
        }
    }

    private void ResetPlates()
    {
        firstPlateTriggered = false;
        secondPlateTriggered = false;
        thirdPlateTriggered = false;
        timer = 0f;

        if (anim != null)
            anim.Play("PressurePlate_Release");
        else
            Debug.LogError("Animator is not assigned or found!");

        if (source != null)
            source.Play();
        else
            Debug.LogError("AudioSource is not assigned or found!");

        Debug.Log("Plates reset.");
    }
}
