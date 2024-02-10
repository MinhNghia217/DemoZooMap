using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInteract : MonoBehaviour
{
    public event Action OnEnterTrigger;
    public event Action OnExitTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnEnterTrigger?.Invoke();
            Debug.Log("Touch");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnExitTrigger?.Invoke();
            Debug.Log("Exit");
        }
    }
}
