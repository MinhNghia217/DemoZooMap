using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public enum BTNodeState
{
    Success,
    Failure,
    Running
}
public abstract class BTNode 
{
    public abstract BTNodeState Evaluate();
}
