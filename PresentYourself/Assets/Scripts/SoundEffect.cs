using UnityEngine;
public class SoundEffect : MonoBehaviour
{
    [SerializeField] private AudioClip clip;

    public void PlaySoundEffect()
    {
        GameEvents.OnSoundEffects.Invoke(clip);
    }
}
