using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;
using WUG.BehaviorTreeVisualizer;

public class GoAroundNode : Node
{
    //----parameter
    private List<Transform> targets;
    private AnimalController animalController;
    //----local
    protected NavMeshAgent agen;
    protected int indexTargets = 0;
    protected Transform transAnimal;
    public GoAroundNode(List<Transform> targets, AnimalController animalController)
    {
        this.targets = targets;
        this.animalController = animalController;
        Init();
    }
    protected void Init()
    {
        transAnimal = animalController.gameObject.transform;
        agen = animalController.MyNavMesh;
    }

    protected override void OnReset() { 
    }

    protected override NodeStatus OnRun()
    {
        if (targets.Count == 0)
        {
            return NodeStatus.Failure;
        }
        Transform target = targets[indexTargets];
        agen.SetDestination(targets[indexTargets].position);
        float distance = Vector3.Distance(target.position, transAnimal.position);//distane from now - targets[i]
        if (distance <= agen.stoppingDistance)
        {
            indexTargets++;
            if (indexTargets >= targets.Count)
            {
                indexTargets = 0;
                return NodeStatus.Success;
            }
        }
        return NodeStatus.Success;
    }
}
