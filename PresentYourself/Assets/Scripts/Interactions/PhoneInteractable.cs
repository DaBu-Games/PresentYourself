using System.Collections;
using UnityEngine;

public class PhoneInteractable : InteractableBase
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private Animation phoneAnimation;
    [SerializeField] private Animation agent2Animation;
    [SerializeField] private GameObject oldPhone;
    
    public override IEnumerator HandleInteraction()
    {
        if(hasInteracted)
            yield break;
        
        phoneAnimation.Play();
        yield return new WaitForSeconds(phoneAnimation.clip.length);
        agent2Animation.Play();
        GameEvents.OnSoundEffects.Invoke(clip);
        yield return new WaitForSeconds(agent2Animation.clip.length);
        oldPhone.SetActive(true);
    }
}
