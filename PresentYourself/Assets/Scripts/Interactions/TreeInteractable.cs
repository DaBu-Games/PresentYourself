using System.Collections;
using UnityEngine;

public class TreeInteractable : InteractableBase
{
    [SerializeField] private Animation shakeTree;
    [SerializeField] private Animation squirrel;
    [SerializeField] private DogState dogState;
    [SerializeField] private SquirrelInteractable squirrelInteractable;
    
    public override IEnumerator HandleInteraction()
    {
        if (!hasInteracted)
        {
            shakeTree.Play();
            yield return new WaitForSeconds(shakeTree.clip.length);
        
            squirrel.Play("squirrel_fall");
            yield return new WaitForSeconds(squirrel["squirrel_fall"].clip.length);
            squirrelInteractable.CanEscape();
        }

        if (dogState.GetCurrentState() == DogStates.Guarding)
        {
            squirrel.Play("squirrel_run");
            dogState.StartChasing();
        }
    }
}
