using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseNode : Node
{
    private AnimalAI AnimalAI;
    private Transform target;
    private NavMeshAgent agent;
    public ChaseNode(Transform targe, AnimalAI animalAI, NavMeshAgent agent)
    {
        this.target = targe;
        AnimalAI = animalAI;
        this.agent = agent;
    }
    public override NodeState Evaluate()
    {
       
        float distance = Vector3.Distance(target.position, AnimalAI.transform.position);
    
        if (distance > 0.2f)
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
   
            return NodeState.RUNNING;
        }
        else
        {
            agent.isStopped = true;
            return NodeState.SUCCESS;
        }
    }


}
