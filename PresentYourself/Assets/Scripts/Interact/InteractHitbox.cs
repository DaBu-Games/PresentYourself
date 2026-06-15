using System;
using UnityEngine;

public class InteractHitbox : MonoBehaviour
{
    [SerializeField] private GameObject interactableObject;
    
    private IInteractable _interactable;

    private void Start()
    {
        _interactable = interactableObject.GetComponent<IInteractable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _interactable.SetRange(true);
            GameEvents.CanInteract?.Invoke(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _interactable.SetRange(false);
            GameEvents.CanInteract?.Invoke(false);
        }
    }
}
