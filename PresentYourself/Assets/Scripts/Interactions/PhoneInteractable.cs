using System.Collections;
using UnityEngine;

public class PhoneInteractable : InteractableBase
{
    [SerializeField] private Transform agent2Phone;
    [SerializeField] private Animation phoneAnimation;
    
    public override IEnumerator HandleInteraction()
    {
        if(hasInteracted)
            yield break;
        
        phoneAnimation.Play();
        yield return new WaitForSeconds(phoneAnimation.clip.length);
        agent2Phone.gameObject.SetActive(true);
    }
}
