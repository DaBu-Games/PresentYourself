using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject klokUI;
    [SerializeField] private GameObject interactUI;
    [SerializeField] private GameObject clickUI;

    private void Start()
    {
        klokUI.SetActive(false);
        interactUI.SetActive(false);
        clickUI.SetActive(false);
        GameEvents.CanInteract += InteractUI;
        GameEvents.KlokInteraction += KlokInteraction;
        GameEvents.CanClick += ClickUI;
    }

    private void OnDisable()
    {
        GameEvents.CanInteract -= InteractUI;
        GameEvents.KlokInteraction -= KlokInteraction;
        GameEvents.CanClick -= ClickUI;
    }

    private void InteractUI(bool canInteract)
    {
        interactUI.SetActive(canInteract);
    }

    private void KlokInteraction(bool canInteract)
    {
        klokUI.SetActive(canInteract);
    }

    private void ClickUI(bool canClick)
    {
        clickUI.SetActive(canClick);
    }
}
