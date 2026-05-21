using System.Collections;
using UnityEngine;

public class GeneratorInteractable : InteractableBase
{
    [SerializeField] private GameObject lights;
    [SerializeField] private Animation agent1Animation;

    public override IEnumerator HandleInteraction()
    {
        lights.gameObject.SetActive(false);
        
        if (hasInteracted)
            yield break;
        
        agent1Animation.Play();
        yield return new WaitForSeconds(agent1Animation.clip.length);
    }
}
