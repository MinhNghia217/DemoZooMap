using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using WUG.BehaviorTreeVisualizer;

public class GoToTargetNode : Condition
{
    private NavigationActivity m_ActivityToCheckFor;
    private NavMeshAgent agen;
    private Transform target;
    public GoToTargetNode(NavigationActivity activity, NavMeshAgent agen, Transform target) : base($"Is Navigation Activity {activity}?")
    {
        m_ActivityToCheckFor = activity;
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
