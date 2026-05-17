using UnityEngine.EventSystems;

public interface IInteractable : IPointerClickHandler
{
    bool hasInteracted { get; }
    InteractableStates interactableState { get; }
}
