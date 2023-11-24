using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_To_Interact : MonoBehaviour, IInteractTable
{
    [SerializeField] private string _prompt;


    public string InteractionPromp => _prompt;

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Cham cai dit me may");
        return true;    
    }
}
