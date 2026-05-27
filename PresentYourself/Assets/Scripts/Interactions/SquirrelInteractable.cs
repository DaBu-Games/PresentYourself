using System.Collections;
using UnityEngine;

public class SquirrelInteractable : InteractableBase
{
    [SerializeField] private Animation squirrel;
    private bool canEscape;
    
    public override IEnumerator HandleInteraction()
    {
        if(!canEscape)
            yield break;
        
        squirrel.Play("squirrel_escape");
        yield return new WaitForSeconds(squirrel["squirrel_escape"].clip.length);
        GameEvents.FinishedLevel.Invoke();
    }
    
    public void CanEscape() => canEscape = true;
}
