using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUG.BehaviorTreeVisualizer;

public class CheckPointNode : Node
{
    private ConfigAnimal configAnimal;

    public CheckPointNode(ConfigAnimal configAnimal)
    {
        this.configAnimal = configAnimal;
    }

    protected override void OnReset(){}

    protected override NodeStatus OnRun()
    {
        
        if (configAnimal.CanInteract) return NodeStatus.Success;
        else return NodeStatus.Failure;
    }
}
