using System;
using UnityEngine;

public static class GameEvents
{
    public static Action<Transform> OnPointerClick;
    public static Action FinishedLevel;
    public static Action<AudioClip> OnSoundEffects;
    public static Action<AudioClip, float> OnSoundEffectsWithVolume;
}
