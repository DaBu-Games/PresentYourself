using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DogToyInteractable : InteractableBase
{
    [SerializeField] private DogState dogState;
    [SerializeField] private AudioClip clip;

    private bool usedDistraction = false;
    
    public override IEnumerator HandleInteraction()
    {
        if (dogState.GetCurrentState() == DogStates.Guarding && !usedDistraction)
        {
            dogState.GetDistracted();
            usedDistraction = true;
        }
        
        GameEvents.OnSoundEffects.Invoke(clip);

        yield return null;
    }
}
