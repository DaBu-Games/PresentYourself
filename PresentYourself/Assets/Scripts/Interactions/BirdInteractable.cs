using System.Collections;
using UnityEngine;

public class BirdInteractable : InteractableBase
{
    [SerializeField] private Animation bird;
    [SerializeField] private DogState dogState;
    [SerializeField] private CannonInteractable cannonInteractable;
    
    private bool deliveredBom = false;
    
    public override IEnumerator HandleInteraction()
    {
        if (deliveredBom)
            yield break;
        
        if (dogState.GetCurrentState() == DogStates.Chasing)
        {
            PlayAnimation("bird_pickUp");
            yield return new WaitForSeconds(bird["bird_pickUp"].length);
            deliveredBom = true;
            cannonInteractable.LoadCannon();
        }
        else
        {
            PlayAnimation("bird_run");
            yield return new WaitForSeconds(bird["bird_run"].length);
        }
        
        yield return null;
    }
    
    private void PlayAnimation(string animName)
    {
        bird.Stop();

        AnimationState state = bird[animName];
        state.time = 0f;
        state.normalizedTime = 0f;

        bird.Play(animName);
    }
}
