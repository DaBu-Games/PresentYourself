using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CalenderWheel : MonoBehaviour
{
    [SerializeField] private TextMeshPro calenderText;
    [SerializeField] private List<string> items = new List<string>();
    [SerializeField] private int index = 0;
    [SerializeField] private int correctIndex = 0;
    
    [Header("Calender buttons")]
    [SerializeField] private CalenderButton minusButton;
    [SerializeField] private CalenderButton plusButton;

    private void Awake()
    {
        minusButton.Initialize(() =>
        {
            index = (index - 1 + items.Count) % items.Count;
            UpdateCalenderText();
        });

        plusButton.Initialize(() =>
        {
            index = (index + 1) % items.Count;
            UpdateCalenderText();
        });

        UpdateCalenderText();
    }

    private void UpdateCalenderText()
    {
        calenderText.text = items[index];
    }
    
    public bool IsCorrect => index == correctIndex;
}
