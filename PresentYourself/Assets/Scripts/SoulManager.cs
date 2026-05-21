using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class SoulManager : MonoBehaviour
{
    [SerializeField] private float transitionDuration = 1f;
    [SerializeField] private float interactRange = 3f;
    [SerializeField] private InteractableBase startInteractable;

    public static bool IsBusy {get; private set;}
    private bool _movementDone;
    private bool _interactionDone;
    
    private void Start()
    {
        GameEvents.OnPointerClick += Transition;
        
        transform.position = startInteractable.transform.position;
        startInteractable.SetState(HighlightState.Selected);
        UpdateInteractableTargets(startInteractable);
    }

    private void OnDestroy()
    {
        GameEvents.OnPointerClick -= Transition;
    }

    private void Transition(Transform obj)
    {
        StartCoroutine(HandleFullTransition(obj));
    }
    
    private IEnumerator HandleFullTransition(Transform obj)
    {
        IsBusy = true;
        transform.SetParent(null);
        
        _movementDone = false;
        _interactionDone = false;
        
        var interactable = obj.GetComponent<InteractableBase>();
        
        StartCoroutine(RunInteraction(interactable));
        
        yield return MoveToTarget(obj);
        
        _movementDone = true;
        
        yield return new WaitUntil(() => _movementDone && _interactionDone);
        
        UpdateInteractableTargets(interactable);
        IsBusy = false;
    }
    
    private IEnumerator RunInteraction(InteractableBase interactable)
    {
        if (interactable == null)
        {
            _interactionDone = true;
            yield break;
        }

        yield return interactable.HandleInteraction();

        _interactionDone = true;
    }
    
    private IEnumerator MoveToTarget(Transform obj)
    {
        IsBusy = true;
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


    private void UpdateInteractableTargets(InteractableBase currentInteractable)
    {
        foreach (InteractableBase interactable in InteractableBase.All)
        {
            if (interactable == currentInteractable)
                continue;
            
            Vector2 offset = interactable.transform.position - transform.position;

            if (Mathf.Abs(offset.x) <= interactRange && Mathf.Abs(offset.y) <= interactRange)
            {
                interactable.SetState(HighlightState.Selectable);
            }
            else
            {
                interactable.SetState(HighlightState.None);
            }
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        Vector3 size = new Vector3(
            interactRange * 2,
            interactRange * 2,
            0.1f);

        Gizmos.DrawWireCube(transform.position, size);
    }
}
