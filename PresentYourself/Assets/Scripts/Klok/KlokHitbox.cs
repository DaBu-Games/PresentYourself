using System;
using UnityEngine;

public class KlokHitbox : MonoBehaviour
{
    [SerializeField] private KlokManager klokManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            klokManager.SetRange(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            klokManager.SetRange(false);
        }
    }
}
