using UnityEngine;

public class HitTarget : MonoBehaviour
{
    public Material hitMaterial; // Assign the material to use when the object is hit
    private Material originalMaterial; // Store the original material
    private Renderer objectRenderer;
    public TargetManager targetManager; // Reference to the TargetManager to notify

    private bool isHit = false; // Ensure each target is only counted once

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalMaterial = objectRenderer.material; // Store the object's original material
        }
    }

    public void OnHit()
    {
        if (!isHit)
        {
            isHit = true;

            Debug.Log(gameObject.name + " was hit!");

            // Change the object's material to the hitMaterial
            if (objectRenderer != null && hitMaterial != null)
            {
                objectRenderer.material = hitMaterial;
            }

            // Notify the target manager
            if (targetManager != null)
            {
                targetManager.TargetHit();
            }
        }
    }
}
