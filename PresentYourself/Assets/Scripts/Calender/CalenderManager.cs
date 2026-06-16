using System;
using System.Collections.Generic;
using UnityEngine;

public class CalenderManager : MonoBehaviour
{
    [SerializeField] private List<CalenderWheel> wheels = new List<CalenderWheel>();
    [SerializeField] private CalenderButton calenderButton;

    private void Awake()
    {
        calenderButton.Initialize(CheckAnswer);
    }

    private void CheckAnswer()
    {
        Debug.Log("check answer");
        
        if (wheels.TrueForAll(w => w.IsCorrect))
        {
            Debug.Log("Correct");
        }
    }
}
