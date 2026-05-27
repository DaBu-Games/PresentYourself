using System.Collections;
using UnityEngine;

public class PhoneInteractable : InteractableBase
{
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
        yield return new WaitForSeconds(agent2Animation.clip.length);
        oldPhone.SetActive(true);
    }
}
