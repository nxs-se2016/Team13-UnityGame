using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public List<HitTarget> hitTargets; // List of targets to track
    public GameObject objectToPopUp;
    public Vector3 popUpPosition;
    public float popUpDuration = 2f; // Duration for the pop-up animation

    private int activatedCount = 0; // Number of targets hit
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

    public void TargetHit()
    {
        activatedCount++;
        CheckAllActivated();
    }

    private void CheckAllActivated()
    {
        if (activatedCount == hitTargets.Count)
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
        Debug.Log("All targets hit. Object popped up.");
    }
}
