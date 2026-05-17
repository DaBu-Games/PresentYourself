using UnityEngine;
using UnityEngine.EventSystems;

public class Generator : MonoBehaviour, IInteractable
{
    public bool hasInteracted { get; private set; }
    public InteractableStates interactableState { get; private set; }
    
    private void Start()
    {
        hasInteracted = false;
        interactableState = InteractableStates.Closed;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if(hasInteracted)
            return;
        
        gameObject.transform.localScale *= 1.1f;
        hasInteracted = true;
        interactableState = InteractableStates.Opened;
    }
}
