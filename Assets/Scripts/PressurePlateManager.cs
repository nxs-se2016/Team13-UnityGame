using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateManager : MonoBehaviour
{
    public List<PressurePlate> pressurePlates;
    public GameObject objectToPopUp;
    public Vector3 popUpPosition;
    public float popUpDuration = 2f; // Duration for the pop-up animation
    private int activatedCount = 0;
    private Vector3 initialPosition;
    private Renderer objectRenderer;

    private void Start()
    {
        initialPosition = objectToPopUp.transform.position;
        objectRenderer = objectToPopUp.GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            objectRenderer.enabled = false; // Disable the renderer to make the object invisible
        }
    }

    public void PressurePlateActivated()
    {
        activatedCount++;
        CheckAllActivated();
    }

    public void PressurePlateDeactivated()
    {
        activatedCount--;
    }

    private void CheckAllActivated()
    {
        if (activatedCount == 1)
        {
            StartCoroutine(PopUpObject());
        }
    }

    private IEnumerator PopUpObject()
    {
        if (objectRenderer != null)
        {
            objectRenderer.enabled = true; // Enable the renderer to make the object visible
        }

        float elapsedTime = 0f;

        while (elapsedTime < popUpDuration)
        {
            objectToPopUp.transform.position = Vector3.Lerp(initialPosition, popUpPosition, elapsedTime / popUpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        objectToPopUp.transform.position = popUpPosition;
        Debug.Log("All pressure plates activated. Object popped up.");
    }
}