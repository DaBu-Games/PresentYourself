using UnityEngine;


public class DogState : MonoBehaviour
{
    [SerializeField] private DogStates currentState = DogStates.Guarding;
    [SerializeField] private Animation dog;

    public void StartChasing()
    {
        currentState = DogStates.Chasing;
        PlayAnimation("dog_chase");
    }

    public void GetDistracted()
    {
        currentState = DogStates.Distracted;
        PlayAnimation("dog_distracted");
    }

    public DogStates GetCurrentState() => currentState;
    
    private void PlayAnimation(string animName)
    {
        dog.Stop();

        AnimationState state = dog[animName];
        state.time = 0f;
        state.normalizedTime = 0f;

        dog.Play(animName);
    }
}

public enum DogStates
{
    Guarding,
    Chasing,
    Distracted
}
