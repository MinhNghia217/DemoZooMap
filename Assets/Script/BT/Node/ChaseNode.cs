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
        if (target == null)
        {
            return NodeState.FAILURE;
        }

        agent.SetDestination(target.position);
        AnimalAI.state = STATE.run;
        Debug.Log(target.gameObject.ToString());
        if (Vector3.Distance(AnimalAI.transform.position, target.position) <= agent.stoppingDistance)
        {
            return NodeState.SUCCESS;
        }

        return NodeState.RUNNING;
    }
    /*float distance = Vector3.Distance(target.position, AnimalAI.transform.position);

    if (distance <= agent.stoppingDistance)
    {
        agent.isStopped = true;
        return NodeState.SUCCESS;
    }
    else
    {

        agent.isStopped = false;
        agent.SetDestination(target.position);
        Debug.Log(target.gameObject.ToString());
        return NodeState.FAILURE;
    }*/

}



