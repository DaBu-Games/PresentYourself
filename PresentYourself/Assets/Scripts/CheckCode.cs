using TMPro;
using UnityEngine;

public class CheckCode : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private int code;
    [SerializeField] private AudioClip clip;

    public void CheckCodeInput()
    {
        if(code.ToString() == inputField.text)
            GameEvents.FinishedLevel.Invoke();
        else
            GameEvents.OnSoundEffects.Invoke(clip);
        
        inputField.text = "";
    }
}
