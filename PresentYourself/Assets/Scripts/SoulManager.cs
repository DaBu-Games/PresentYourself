using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SoulManager : MonoBehaviour
{
    [SerializeField] private float transitionDuration = 1f;
    
    private void Start()
    {
        GameEvents.OnPointerClick += Transition;
    }

    private void Transition(Transform obj)
    {
        StartCoroutine(MoveToTarget(obj));
    }
    
    private IEnumerator MoveToTarget(Transform obj)
    {
        transform.SetParent(null);

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = obj.position;

        float time = 0f;

        while (time < 1f)
        {
            time += Time.deltaTime / transitionDuration;

            transform.position = Vector3.Lerp(startPosition, targetPosition, time);

            yield return null;
        }

        transform.position = targetPosition;
        transform.SetParent(obj);
    }
}
