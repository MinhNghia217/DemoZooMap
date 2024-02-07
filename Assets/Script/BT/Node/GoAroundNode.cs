using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class GoAroundNode : Node
{
    private AnimalAI animalAI;
    private List<Transform>  targets;
    private NavMeshAgent agent;
    //---Flag---
    private int currentTargetIndex=0;
    public GoAroundNode(List<Transform> targets, NavMeshAgent agent, AnimalAI animalAI)
    {
        this.targets = targets;
        this.agent = agent;
        this.animalAI = animalAI;

        currentTargetIndex = 0;
    }
    public override NodeState Evaluate()
    {
        if (targets.Count == 0)
        {
            return NodeState.FAILURE;
        }
        Transform target = targets[currentTargetIndex];
        agent.SetDestination(targets[currentTargetIndex].position);
        Debug.Log("run");
        animalAI.state = STATE.run;

        if (Vector3.Distance(animalAI.transform.position, target.position) <= agent.stoppingDistance)
        {
            Debug.Log("touch");
            currentTargetIndex++;
            if (currentTargetIndex >= targets.Count)
            {
                currentTargetIndex = 0;
                return NodeState.SUCCESS;
            }
        }


        return NodeState.RUNNING;
    }
}
