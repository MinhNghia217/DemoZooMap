using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using WUG.BehaviorTreeVisualizer;

public class GoToTargetNode : Node
{
    private NavMeshAgent agen;
    private Transform target;
    public GoToTargetNode( NavMeshAgent agen, Transform target) 
    {
        this.agen = agen;
        this.target = target;
    }

    protected override void OnReset()
    {

    }

    protected override NodeStatus OnRun()
    {
      
        agen.SetDestination(target.position);   
        return NodeStatus.Success;
    }

 
}
