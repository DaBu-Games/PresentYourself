using UnityEngine;

public class FollowUpAnimation : MonoBehaviour
{
    [SerializeField] private Animation followUpAnimation;

    public void StartFollowUpAnimation()
    {
        followUpAnimation.Play();
    }
}
