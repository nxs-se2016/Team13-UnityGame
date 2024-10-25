using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private string _interactionPrompt;

    public string InteractionPrompt => _interactionPrompt;

    public bool Interact(Interactor interactor)
    {
        var inventory = interactor.GetComponent<Inventory>();

        if(inventory == null){
            return false;
        }

        if(inventory.HasKey){
            Debug.Log("Chest Opened");
            return true;
        }
        Debug.Log("Chest Locked");
        return true;
    }
}