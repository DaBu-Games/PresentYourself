using System;
using UnityEngine;

public static class GameEvents
{
    public static Action<bool> CanInteract;
    public static Action<bool> CanClick;
    public static Action<bool> KlokInteraction;
    
    public static Action CompletedPuzzle;
    public static Action FalseAnswer;
    public static Action EndCredits;
    
    public static Action<bool> OnSoundEffectsPuzzle;
    public static Action<bool> OnColorTransition;
}
