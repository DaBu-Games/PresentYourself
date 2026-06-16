using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScaleWeight : MonoBehaviour, IClickable
{
    [SerializeField] private int weight;
    [SerializeField] private string weightText;
    [SerializeField] private List<TextMeshPro> weightTexts;
    public bool isInsideScale = false;
    
    public int GetWeight => weight;

    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Awake()
    {
        foreach (TextMeshPro textMeshPro in weightTexts)
        {
            textMeshPro.text = weightText;
        }
    }

    public void ResetPosition()
    {
        transform.position = _startPosition;
        transform.rotation = Quaternion.identity;
        transform.parent = null;
    }

    public void OnClick()
    {
        return;
    }
}
