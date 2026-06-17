using System.Collections.Generic;
using UnityEngine;

public class ScaleManager : MonoBehaviour
{
    [Header("scale values")]
    [SerializeField] private int scaleAnswer = 8;
    [SerializeField] private int maxRotation = 40;

    [Header("scale transforms")]
    [SerializeField] private Transform scaleTransform;
    
    [Header("slots")]
    [SerializeField] private List<Transform> slots;
    private ScaleWeight[] _slotOccupants;

    private int _currentWeight;
    
    private float _currentRotation;
    private float _targetRotation;

    public bool IsCompleted { get; private set; }

    private readonly HashSet<ScaleWeight> _weightsOnScale = new();
    
    private void Start()
    {
        Recalculate();
        IsCompleted = false;
    }
    
    private void Awake()
    {
        _slotOccupants = new ScaleWeight[slots.Count];
    }
    
    private void Update()
    {
        _currentRotation = Mathf.Lerp(_currentRotation, _targetRotation, Time.deltaTime * 5f);
        scaleTransform.localRotation = Quaternion.Euler(0, 0, _currentRotation);
    }

    public bool TryAddWeight(ScaleWeight weight)
    {
        if (_weightsOnScale.Contains(weight) || !weight.isInsideScale)
            return false;

        for (int i = 0; i < slots.Count; i++)
        {
            if (_slotOccupants[i] == null)
            {
                _slotOccupants[i] = weight;
                _weightsOnScale.Add(weight);

                weight.transform.position = slots[i].position;
                weight.transform.SetParent(slots[i]);

                _currentWeight += weight.GetWeight;
                Recalculate();

                return true;
            }
        }

        return false;
    }

    public void RemoveWeight(ScaleWeight weight)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (_slotOccupants[i] == weight)
            {
                _slotOccupants[i] = null;
                _weightsOnScale.Remove(weight);

                _currentWeight -= weight.GetWeight;
                Recalculate();
                return;
            }
        }
    }

    private void Recalculate()
    {
        float net = _currentWeight - scaleAnswer;
        
        float percent = Mathf.Clamp(net / scaleAnswer, -1f, 1f);

        _targetRotation = -percent * maxRotation;

        if (_currentWeight == scaleAnswer)
        {
            IsCompleted = true;
            GameEvents.CompletedPuzzle.Invoke();
        }
    }
}