using UnityEngine;

public class Gun : MonoBehaviour
{
    public Camera playerCamera; // Assign the player's camera here
    public float shootingRange = 1000f;
    public LayerMask hitLayers; // Set this to the layer(s) the gun can interact with
    public Transform gunHoldPosition; // The transform where the gun is held by the player

    private bool isPickedUp = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isPickedUp)
            {
                DropGun();
            }
            else
            {
                CheckForPickup();
            }
        }

        if (isPickedUp)
        {
            AimAndShoot();
        }
    }

    void CheckForPickup()
    {
        float distance = Vector3.Distance(transform.position, gunHoldPosition.position);
        if (distance < 10f) // Check if the player is close enough to pick up the gun
        {
            PickUpGun();
        }
    }

    void PickUpGun()
    {
        isPickedUp = true;
        transform.SetParent(gunHoldPosition); // Attach gun to the player
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        GetComponent<Rigidbody>().isKinematic = true; // Disable physics while the gun is held
    }

    void DropGun()
    {
        isPickedUp = false;
        transform.SetParent(null); // Detach the gun from the player
        GetComponent<Rigidbody>().isKinematic = false; // Re-enable physics so the gun falls naturally
        Debug.Log("Gun dropped!");
    }

    void AimAndShoot()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button to shoot
        {
            Debug.Log("Shooting!");
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, shootingRange, hitLayers))
            {
                Debug.Log("Hit: " + hit.collider.name);

                // Check if the object hit has the HitTarget script
                HitTarget target = hit.collider.GetComponent<HitTarget>();
                if (target != null)
                {
                    target.OnHit();
                }
            }
        }
    }
}
