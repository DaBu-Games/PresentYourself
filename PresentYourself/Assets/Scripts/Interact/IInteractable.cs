using UnityEngine;

public interface IInteractable
{
    void SetRange(bool isInRange);
    void EnterInteraction();
    void ExitInteraction();
}
