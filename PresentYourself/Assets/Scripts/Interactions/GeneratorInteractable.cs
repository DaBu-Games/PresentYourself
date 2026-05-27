using System.Collections;
using UnityEngine;

public class GeneratorInteractable : InteractableBase
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private GameObject lights;
    [SerializeField] private Animation agent1Animation;

    public override IEnumerator HandleInteraction()
    {
        if (hasInteracted)
            yield break;
        
        GameEvents.OnSoundEffects.Invoke(clip);
        lights.gameObject.SetActive(false);
        
        agent1Animation.Play();
        yield return new WaitForSeconds(agent1Animation.clip.length);
    }
}
