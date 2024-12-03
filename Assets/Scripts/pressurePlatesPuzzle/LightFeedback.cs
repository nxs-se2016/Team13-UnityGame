using System.Collections;
using UnityEngine;

public class LightFeedback : MonoBehaviour
{
    private Light feedbackLight;

    public float flashDuration = 1f; // Total duration of the flash
    public int flashCount = 3;      // Number of flashes during the duration

    private void Start()
    {
        feedbackLight = GetComponent<Light>();
        if (feedbackLight == null)
        {
            Debug.LogError("No Light component found on the GameObject!");
        }
        else
        {
            feedbackLight.enabled = false; // Turn off initially
            Debug.Log("LightFeedback initialized successfully.");
        }
    }

    public void FlashLight()
    {
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        float flashInterval = flashDuration / (flashCount * 2); // On/Off duration for each flash

        for (int i = 0; i < flashCount; i++)
        {
            feedbackLight.enabled = true; // Turn light on
            Debug.Log("Light ON");
            yield return new WaitForSeconds(flashInterval);

            feedbackLight.enabled = false; // Turn light off
            Debug.Log("Light OFF");
            yield return new WaitForSeconds(flashInterval);
        }
    }
}
