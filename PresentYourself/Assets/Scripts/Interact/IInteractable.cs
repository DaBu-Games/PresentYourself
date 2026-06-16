using UnityEngine;

public interface IInteractable
{
    bool IsCompleted { get; }
    
    void SetRange(bool isInRange);
    void EnterInteraction();
    void ExitInteraction();
}
