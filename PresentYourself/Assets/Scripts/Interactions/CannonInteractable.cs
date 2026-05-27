using System.Collections;
using UnityEngine;

public class CannonInteractable : InteractableBase
{
    private bool isLoaded;
    
    public override IEnumerator HandleInteraction()
    {
        if(!isLoaded)
            yield break;
        
        GameEvents.FinishedLevel.Invoke();
    }
    
    public void LoadCannon() => isLoaded = true;
}
