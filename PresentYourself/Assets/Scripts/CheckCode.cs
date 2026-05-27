using TMPro;
using UnityEngine;

public class CheckCode : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private int code;

    public void CheckCodeInput()
    {
        if(code.ToString() == inputField.text)
            GameEvents.FinishedLevel.Invoke();
        
        inputField.text = "";
    }
}
