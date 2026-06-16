using System;
using UnityEngine;

public class ScaleWeight : MonoBehaviour
{
    [SerializeField] private int weight;
    public bool isInsideScale = false;
    
    public int GetWeight => weight;

    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    public void ResetPosition()
    {
        transform.position = _startPosition;
        transform.rotation = Quaternion.identity;
        transform.parent = null;
    }
}
