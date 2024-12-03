using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupClass : MonoBehaviour
{
    [SerializeField] private LayerMask PickupLayer;
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private float PickupRange = 5f;

    [SerializeField] private Transform Hand;

    private Rigidbody CurrentObjectRgidbody;
    private Collider CurrentObjectCollider;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Ray Pickupray = new Ray(PlayerCamera.transform.position, PlayerCamera.transform.forward);

            if (Physics.Raycast(Pickupray, out RaycastHit hitInfo, PickupRange, PickupLayer))
            {
                if (CurrentObjectRgidbody)
                {

                }
                else
                {
                    CurrentObjectRgidbody = hitInfo.rigidbody;
                    CurrentObjectCollider = hitInfo.collider;

                    CurrentObjectRgidbody.isKinematic = true;
                    CurrentObjectCollider.enabled = false;
                }

            }
        }
        if (CurrentObjectRgidbody){
            CurrentObjectRgidbody.position = Hand.position;
            CurrentObjectRgidbody.rotation = Hand.rotation;

        }

    }
}
