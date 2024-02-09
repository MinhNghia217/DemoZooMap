using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUG.BehaviorTreeVisualizer;
using static UnityEngine.UI.Image;

public class InRangeNode : Node
{

    private float _ranger;
    private AnimalController AnimalController;
    private Transform target;
    public InRangeNode( float _ranger, AnimalController AnimalController, Transform target) 
    {
        this._ranger = _ranger;
        this.AnimalController = AnimalController;
        this.target = target;
    }

    protected override void OnReset()
    {
        
    }

    protected override NodeStatus OnRun()
    {
        Vector3 tempTarget = new Vector3(target.position.x, AnimalController.gameObject.transform.position.y, target.position.z);
        float distance = Vector3.Distance(tempTarget, AnimalController.gameObject.transform.position);
        return distance <= _ranger ? NodeStatus.Success : NodeStatus.Failure;
    }


}
