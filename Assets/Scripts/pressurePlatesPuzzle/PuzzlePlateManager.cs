using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePlateManager : MonoBehaviour
{
    public GameObject objectToPopUp; // The treasure chest or object to pop up
    public Vector3 popUpPosition; // Final position of the chest
    public float popUpDuration = 2f; // Duration for the pop-up animation

    private Vector3 initialPosition; // Starting position of the chest
    private Renderer objectRenderer; // For visibility control

    private void Start()
    {
        initialPosition = objectToPopUp.transform.position;
        Debug.Log($"Initial position set to: {initialPosition}");

        objectRenderer = objectToPopUp.GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            objectRenderer.enabled = false; // Make the chest invisible initially
        }
    }

    public void StartPopUp()
    {
        StartCoroutine(PopUpObject());
    }

    private IEnumerator PopUpObject()
    {
        if (objectRenderer != null)
        {
            objectRenderer.enabled = true; // Make the chest visible
        }

        float elapsedTime = 0f;

        while (elapsedTime < popUpDuration)
        {
            objectToPopUp.transform.position = Vector3.Lerp(initialPosition, popUpPosition, elapsedTime / popUpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        objectToPopUp.transform.position = popUpPosition;
        Debug.Log("Treasure chest has popped up!");
    }
}
