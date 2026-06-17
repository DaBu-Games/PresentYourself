using System;
using System.Collections.Generic;
using UnityEngine;

public class CalenderManager : MonoBehaviour
{
    [SerializeField] private List<CalenderWheel> wheels = new List<CalenderWheel>();
    [SerializeField] private CalenderButton calenderButton;

    private bool _isCompleted;

    private void Awake()
    {
        calenderButton.Initialize(CheckAnswer);
    }

    private void CheckAnswer()
    {
        if (_isCompleted)
            return;
        
        if (wheels.TrueForAll(w => w.IsCorrect))
        {
            foreach (CalenderWheel wheel in wheels)
            {
                wheel.IsCompleted();
            }
            
            _isCompleted = true;
            GameEvents.CompletedPuzzle.Invoke();
        }
        else
        {
            GameEvents.FalseAnswer.Invoke();
        }
    }
}
