using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
   [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionRange = 0.5f;
    [SerializeField] private LayerMask _interactableLayer;
    [SerializeField] private InteractionPromptUI _interactionPromptUI;

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFoundColliders;

    private IInteractable _interactable;


    private void Update()
    {
        //Finds everything at this position within a radius of 0.5f returns number of things found
        _numFoundColliders = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionRange, _colliders, _interactableLayer);
        if (_numFoundColliders > 0){
            _interactable = _colliders[0].GetComponent<IInteractable>();
            
            if(_interactable != null){
                _interactionPromptUI.SetUp(_interactable.InteractionPrompt);
                if (Input.GetKeyDown(KeyCode.E)){
                    _interactable.Interact(this);
                }
            }
        }
         else
        {
            if(_interactionPromptUI.IsDisplayed)
            {
                _interactionPromptUI.Close();
            }
            if(_interactable != null)
            {
                if (_interactable is CodeChest codeChest)
                {
                    codeChest.OnInteractionExit();
                }
                _interactable = null;
            }
        }
        

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionRange);
    }
}
