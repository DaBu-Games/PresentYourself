using UnityEngine;
using UnityEngine.EventSystems;

public class Generator : InteractableBase
{
    [SerializeField] private GameObject lights;
    [SerializeField] private Animation agent1Animation;
    
    protected override void HandleClick(PointerEventData eventData)
    {
        if (hasInteracted)
            return;
        
        agent1Animation.Play();
        lights.gameObject.SetActive(false);
    }
}
