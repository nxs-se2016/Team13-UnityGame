using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PressurePlateScript : MonoBehaviour
{
    public string firstPlateTag = "FirstPlate";
    public string otherPlateTag = "OtherPlate";
    public float timeLimit = 60f;

    private bool firstPlateTriggered = false;
    private bool secondPlateTriggered = false;
    private bool thirdPlateTriggered = false;
    private float timer = 0f;

    [SerializeField] public string[] options;
    [SerializeField] public int index;
    [SerializeField] private Animator myAnimator;

    public UnityEvent TagCollisionEnter;
    public UnityEvent TagCollisionExit;
    public UnityEvent TagCollisionStay;
    public bool ShowCollisionEnterEvent = true;
    public bool ShowCollisionStayEvent = true;
    public bool ShowCollisionExitEvent = true;
    public bool DisableAudio = false;
    public bool DisableAnimations = false;
    public bool isInteractive = true;

    private Animator anim;
    private AudioSource source;

    public UnityEvent onTimerStart;
    public UnityEvent onTimerFail;
    public UnityEvent onAllPlatesComplete;

    private void Start()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();

        if (anim == null)
            Debug.LogError("Animator component not found on the pressure plate!");
        if (source == null)
            Debug.LogError("AudioSource component not found on the pressure plate!");

        if (anim == null)
        {
            anim = GetComponentInChildren<Animator>();
            if (anim == null) Debug.LogError("Animator component not found!");
        }
    }

    private void OnEnable()
    {
        InitializeOptions();
        Debug.Log("PressurePlateScript options initialized with " + options.Length + " tags.");
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        InitializeOptions();
    }

    private void InitializeOptions()
    {
        options = UnityEditorInternal.InternalEditorUtility.tags;
        if (options.Length > 0 && index >= options.Length)
        {
            index = 0;
        }

        EditorApplication.update -= UpdateInEditor;
        EditorApplication.update += UpdateInEditor;
    }

    private void UpdateInEditor()
    {
        if (!Application.isPlaying && this != null)
        {
            EditorUtility.SetDirty(this);
        }
        else
        {
            EditorApplication.update -= UpdateInEditor;
        }
    }
#endif

    private void OnDestroy()
    {
#if UNITY_EDITOR
        EditorApplication.update -= UpdateInEditor;
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
        if (other.CompareTag("Player"))
        {
            if (!firstPlateTriggered)
            {
                firstPlateTriggered = true;
                myAnimator.SetTrigger("Press"); // Trigger the Press animation
                Debug.Log("First plate triggered!");
                timer = timeLimit;
                onTimerStart?.Invoke();
            }
            else if (firstPlateTriggered && !secondPlateTriggered)
            {
                secondPlateTriggered = true;
                TriggerPlateAnimation("Press");
                Debug.Log("Second plate triggered!");
            }
            else if (secondPlateTriggered && !thirdPlateTriggered)
            {
                thirdPlateTriggered = true;
                TriggerPlateAnimation("Press");
                Debug.Log("Third plate triggered!");

                if (firstPlateTriggered && secondPlateTriggered && thirdPlateTriggered)
                {
                    Debug.Log("All plates triggered successfully.");
                    onAllPlatesComplete?.Invoke();
                }
            }
        }
    }

    private void TriggerPlateAnimation(string triggerName)
    {
        if (anim != null)
        {
            bool parameterExists = false;
            foreach (var param in anim.parameters)
            {
                if (param.name == triggerName && param.type == AnimatorControllerParameterType.Trigger)
                {
                    parameterExists = true;
                    break;
                }
            }

            if (parameterExists)
            {
                anim.SetTrigger(triggerName);
                Debug.Log($"Animator trigger '{triggerName}' activated.");
            }
            else
            {
                Debug.LogError($"Animator parameter '{triggerName}' not set or Animator is null!");
            }
        }
        else
        {
            Debug.LogError("Animator is null!");
        }
    }

    private void ResetPlates()
    {
        firstPlateTriggered = false;
        secondPlateTriggered = false;
        thirdPlateTriggered = false;
        timer = 0f;

        if (anim != null && anim.HasParameter("Release"))
        {
            anim.SetTrigger("Release");
            Debug.Log("Plates reset - Release Trigger set.");
        }
        else
        {
            Debug.LogError("Animator parameter 'Release' not set or Animator is null!");
        }

        source?.Play();
    }
}
