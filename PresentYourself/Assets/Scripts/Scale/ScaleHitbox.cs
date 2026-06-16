using UnityEngine;

public class ScaleHitbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ScaleWeight weight))
        {
            weight.isInsideScale = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ScaleWeight weight))
        {
            weight.isInsideScale = false;
        }
    }
}
