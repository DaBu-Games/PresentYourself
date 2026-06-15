using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject klokUI;
    [SerializeField] private GameObject interactUI;

    private void Start()
    {
        klokUI.SetActive(false);
        interactUI.SetActive(false);
        GameEvents.CanInteract += InteractUI;
        GameEvents.KlokInteraction += KlokInteraction;
    }

    private void OnDisable()
    {
        GameEvents.CanInteract -= InteractUI;
        GameEvents.KlokInteraction -= KlokInteraction;
    }

    private void InteractUI(bool canInteract)
    {
        interactUI.SetActive(canInteract);
    }

    private void KlokInteraction(bool canInteract)
    {
        klokUI.SetActive(canInteract);
    }
}
