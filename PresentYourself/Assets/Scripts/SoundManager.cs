using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        GameEvents.OnSoundEffects += PlaySound;
        GameEvents.OnSoundEffectsWithVolume += PlaySound;
    }

    private void OnDestroy()
    {
        GameEvents.OnSoundEffects -= PlaySound;
        GameEvents.OnSoundEffectsWithVolume -= PlaySound;
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip == null)
            return;

        audioSource.PlayOneShot(clip);
    }

    private void PlaySound(AudioClip clip, float volume)
    {
        if (clip == null)
            return;

        audioSource.PlayOneShot(clip, volume);
    }
}
