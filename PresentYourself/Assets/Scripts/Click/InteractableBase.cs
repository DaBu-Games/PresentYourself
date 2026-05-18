using UnityEngine;
using UnityEngine.EventSystems;

public abstract class InteractableBase : MonoBehaviour, IPointerClickHandler
{
    protected bool hasInteracted = false;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        GameEvents.OnPointerClick.Invoke(transform);
        
        HandleClick(eventData);
        
        hasInteracted = true;
    }

    protected abstract void HandleClick(PointerEventData eventData);
}
