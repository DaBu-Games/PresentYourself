using System.Collections;
using UnityEngine;

public class CannonInteractable : InteractableBase
{
    [SerializeField] private AudioClip clip;
    private bool isLoaded;
    
    public override IEnumerator HandleInteraction()
    {
        if(!isLoaded)
            yield break;
        
        GameEvents.OnSoundEffects.Invoke(clip);
        GameEvents.FinishedLevel.Invoke();
    }
    
    public void LoadCannon() => isLoaded = true;
}
