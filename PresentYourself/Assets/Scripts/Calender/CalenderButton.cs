using System;
using UnityEngine;

public class CalenderButton : MonoBehaviour, IClickable
{
    private Action _buttonAction;
    public void Initialize(Action buttonAction) => _buttonAction = buttonAction;
    public void OnClick() => _buttonAction.Invoke();
}
