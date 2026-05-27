using System.Collections;
using UnityEngine;

public class AgentInteractable : InteractableBase
{
    [SerializeField] private Transform _camera;
    private Transform _oldTarget;

    public override IEnumerator HandleInteraction()
    {
        SoulManager.InFocusMode = true;
        _camera.gameObject.SetActive(true);
        return null;
    }

    public void UnFocus()
    {
        SoulManager.InFocusMode = false;
        _camera.gameObject.SetActive(false);
        SetState(HighlightState.Selectable);
    }
}
