using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class PressurePlate : MonoBehaviour
{
    public GameObject objectToActivate;
    private Vector3 originalPosition;
    public float moveDownDistance = 0.5f;

    public PressurePlateManager manager;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickuppable"))
        {

            var objectNumber = ExtractNumberFromName(other.name);
            var plateNumber = ExtractNumberFromName(gameObject.name);

            if (objectNumber == plateNumber)
            {
                //Debug.Log("Numbers match: " + objectNumber);
                Debug.Log("Object entered pressure plate: " + other.name);
                Activate();
                manager.PressurePlateActivated();
            }
            else
            {
                Debug.Log("Numbers do not match. Object number: " + objectNumber + ", Plate number: " + plateNumber);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pickuppable"))
        {


            var objectNumber = ExtractNumberFromName(other.name);
            var plateNumber = ExtractNumberFromName(gameObject.name);

            if (objectNumber == plateNumber)
            {
                //Debug.Log("Numbers match: " + objectNumber);
                Debug.Log("Object exited pressure plate: " + other.name);
                Deactivate();
                manager.PressurePlateDeactivated();
            }
            else
            {
                Debug.Log("Numbers do not match. Object number: " + objectNumber + ", Plate number: " + plateNumber);
            }
        }
    }

    private string ExtractNumberFromName(string name)
    {
        var match = Regex.Match(name, @"\d+");
        return match.Success ? match.Value : string.Empty;
    }

    private void Activate()
    {
        if (objectToActivate != null)
        {
            Debug.Log("Activating object: " + objectToActivate.name);
            objectToActivate.SetActive(true);
        }
        MovePlateDown();
    }

    private void Deactivate()
    {
        if (objectToActivate != null)
        {
            Debug.Log("Deactivating object: " + objectToActivate.name);
            objectToActivate.SetActive(false);
        }

        MovePlateUp();
    }

    private void MovePlateDown()
    {
        transform.position = originalPosition - new Vector3(0, moveDownDistance, 0);
    }

    private void MovePlateUp()
    {
        transform.position = originalPosition;
    }

}