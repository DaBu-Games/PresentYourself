using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int totalPuzzles;
    
    private int _completedPuzzles = 0;
    
    private void Start()
    {
        GameEvents.CompletedPuzzle += CompletePuzzleHandler;
        GameEvents.FalseAnswer += FalseAnswerHandler;
    }

    private void OnDestroy()
    {
        GameEvents.CompletedPuzzle -= CompletePuzzleHandler;
        GameEvents.FalseAnswer -= FalseAnswerHandler;
    }

    private void CompletePuzzleHandler()
    {
        _completedPuzzles += 1;
        GameEvents.OnSoundEffectsPuzzle.Invoke(true);

        if (_completedPuzzles >= totalPuzzles)
        {
            GameEvents.EndCredits.Invoke();
        }
        else
        {
            GameEvents.OnColorTransition.Invoke(true);
        }
    }

    private void FalseAnswerHandler()
    {
        GameEvents.OnSoundEffectsPuzzle.Invoke(false);
        GameEvents.OnColorTransition.Invoke(false);
    }
}
