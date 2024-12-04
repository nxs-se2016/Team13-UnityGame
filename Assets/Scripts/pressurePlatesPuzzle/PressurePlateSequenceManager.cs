using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlateSequenceManager : MonoBehaviour
{
    [SerializeField] private List<SinglePressurePlate> pressurePlates; // List of all pressure plates in the order they need to be triggered
    [SerializeField] private UnityEvent onCorrectSequence;
    [SerializeField] private UnityEvent onIncorrectSequence;

    private int currentPlateIndex = 0;

    private void Start()
    {
        // Subscribe to the event of each pressure plate
        foreach (var plate in pressurePlates)
        {
            plate.onPlatePressed.AddListener(() => CheckPlateOrder(plate));
        }
    }

    private void CheckPlateOrder(SinglePressurePlate triggeredPlate)
    {
        // Check if the triggered plate is the correct one in the sequence
        if (triggeredPlate == pressurePlates[currentPlateIndex])
        {
            currentPlateIndex++;
            Debug.Log("Correct plate triggered!");

            // Check if the sequence is complete
            if (currentPlateIndex >= pressurePlates.Count)
            {
                onCorrectSequence.Invoke(); // Trigger event for correct sequence
                Debug.Log("Correct sequence completed!");
                ResetSequence(); // Optionally reset or disable plates here
            }
        }
        else
        {
            Debug.Log("Incorrect plate triggered! Resetting sequence.");
            onIncorrectSequence.Invoke(); // Trigger event for incorrect sequence
            ResetSequence();
        }
    }

    private void ResetSequence()
    {
        currentPlateIndex = 0;

        // Optionally, reset each plate's state if needed
        foreach (var plate in pressurePlates)
        {
            plate.ResetPlate(); // This method can reset the animation/state on the plate
        }
    }
}
