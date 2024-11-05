using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private string _lockedPrompt = "Locked (E)";
    [SerializeField] private string _unlockedPrompt = "Open (E)";

    public string InteractionPrompt 
    { 
        get 
        {
            // Find the Interactor's inventory and check if they have a key
            var inventory = FindObjectOfType<Inventory>();
            return inventory != null && inventory.HasKey ? _unlockedPrompt : _lockedPrompt;
        }
    }

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