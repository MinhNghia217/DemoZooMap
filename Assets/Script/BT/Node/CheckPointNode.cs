using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUG.BehaviorTreeVisualizer;

public class CheckPointNode : Node
{
    private bool Checking;

    public CheckPointNode(bool checking)
    {
        this.Checking = checking;
    }

    protected override void OnReset(){}

    protected override NodeStatus OnRun()
    {
        if (Checking) return NodeStatus.Success;
        else return NodeStatus.Failure;
    }
}
