using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlateSequenceManager : MonoBehaviour
{
    [SerializeField] private List<SinglePressurePlate> pressurePlates; // List of all pressure plates in the order they need to be triggered
    [SerializeField] private UnityEvent onCorrectSequence;
    [SerializeField] private UnityEvent onIncorrectSequence;

    private int currentPlateIndex = 0;
    public PressurePlateManager pressurePlateManager;

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
        if (triggeredPlate == pressurePlates[currentPlateIndex])
        {
            currentPlateIndex++;
            Debug.Log("Correct plate triggered!");

            if (currentPlateIndex >= pressurePlates.Count)
            {
                onCorrectSequence.Invoke();
                Debug.Log("Correct sequence completed!");

                // Trigger the treasure chest animation
                if (pressurePlateManager != null)
                {
                    pressurePlateManager.StartPopUp();
                }

                ResetSequence(); // Optionally reset or disable plates here
            }
        }
        else
        {
            Debug.Log("Incorrect plate triggered! Resetting sequence.");
            onIncorrectSequence.Invoke();
            ResetSequence();
        }
    }

    private void ResetSequence()
    {
        currentPlateIndex = 0;

        // Reset each plate to its initial state
        foreach (var plate in pressurePlates)
        {
            plate.ResetPlate();
        }
    }
}
