using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject klokUI;
    [SerializeField] private GameObject interactUI;
    [SerializeField] private GameObject clickUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private Animation endCredits;
    
    private InputAction _pauseAction;

    public void PauseEvent(bool paused)
    {
        GameEvents.PauseUI.Invoke(paused);
    }

    private void Start()
    {
        klokUI.SetActive(false);
        interactUI.SetActive(false);
        clickUI.SetActive(false);
        pauseUI.SetActive(false);
        GameEvents.CanInteract += InteractUI;
        GameEvents.KlokInteraction += KlokInteraction;
        GameEvents.CanClick += ClickUI;
        GameEvents.PauseUI += PauseUI;
        GameEvents.EndCredits += CreditsAnimation;
        
        _pauseAction = InputSystem.actions.FindAction("Pause");
        _pauseAction.Enable();
        _pauseAction.performed += ctx => PauseEvent(true);
    }

    private void OnDisable()
    {
        GameEvents.CanInteract -= InteractUI;
        GameEvents.KlokInteraction -= KlokInteraction;
        GameEvents.CanClick -= ClickUI;
        GameEvents.PauseUI -= PauseUI;
        GameEvents.EndCredits -= CreditsAnimation;
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

    private void CreditsAnimation()
    {
        endCredits.Play();
    }

    private void PauseUI(bool paused)
    {
        pauseUI.SetActive(paused);
    }
}
