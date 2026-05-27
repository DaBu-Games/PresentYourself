using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    private void Start()
    {
        GameEvents.FinishedLevel += ShowScreen;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameEvents.FinishedLevel -= ShowScreen;
    }

    private void ShowScreen()
    {
        gameObject.SetActive(true); 
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
