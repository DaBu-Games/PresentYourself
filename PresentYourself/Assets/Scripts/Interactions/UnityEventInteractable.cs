using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class UnityEventInteractable : InteractableBase
{
    [SerializeField] private UnityEvent onInteract;
    [SerializeField] private UnityEvent onExit;

    public override IEnumerator HandleInteraction()
    {
        onInteract.Invoke();

        yield return null;
    }

    protected override void OnExitSelected(Transform newTransform)
    {
        base.OnExitSelected(newTransform);
        onExit.Invoke();
        Debug.Log("exit");
    }
}