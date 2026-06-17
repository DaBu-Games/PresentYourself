using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip completedPuzzle;
    [SerializeField] private AudioClip lockedPuzzle;
    

    private void Start()
    {
        GameEvents.OnSoundEffectsPuzzle += PlaySound;
    }

    private void OnDestroy()
    {
        GameEvents.OnSoundEffectsPuzzle -= PlaySound;
    }

    private void PlaySound(bool isCompleted)
    {
        audioSource.PlayOneShot(isCompleted ? completedPuzzle : lockedPuzzle);
    }
}
