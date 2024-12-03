using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlateSequenceManager : MonoBehaviour
{
    [SerializeField] private List<SinglePressurePlate> pressurePlates; // List of plates in sequence
    [SerializeField] private UnityEvent onCorrectSequence;
    [SerializeField] private UnityEvent onIncorrectSequence;

    public LightFeedback redLightFeedback; // Feedback for incorrect sequence

    private int currentPlateIndex = 0; // Tracks the current sequence step

    private void Start()
    {
        foreach (var plate in pressurePlates)
        {
            plate.onPlatePressed.AddListener(() => CheckPlateOrder(plate));
        }
    }

    // Make this method public so it appears in Unity's dropdown
    public void CheckPlateOrder(SinglePressurePlate triggeredPlate)
    {
        Debug.Log($"Triggered Plate: {triggeredPlate.name}, Expected Plate: {pressurePlates[currentPlateIndex].name}");

        if (triggeredPlate == pressurePlates[currentPlateIndex])
        {
            currentPlateIndex++;
            Debug.Log($"Correct plate triggered! Current index is now {currentPlateIndex}.");

            if (currentPlateIndex >= pressurePlates.Count)
            {
                Debug.Log("Correct sequence completed!");
                onCorrectSequence.Invoke();
                ResetPlates();
            }
        }
        else
        {
            Debug.LogWarning("Incorrect plate triggered! Resetting sequence."); // Changed from LogError
            if (redLightFeedback != null)
            {
                redLightFeedback.FlashLight();
            }

            ResetPlates();
        }
    }

    private void ResetPlates()
    {
        currentPlateIndex = 0; // Reset the sequence index

        foreach (var plate in pressurePlates)
        {
            plate.ResetPlate(); // Reset each plate to its initial state
        }

        Debug.Log("Pressure plates have been reset.");
    }
}
