using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RangeNode : Node
{
    private float range;
    private Transform target;
    private Transform origin;

    public RangeNode(float range, Transform target, Transform origin)
    {
        this.range = range;
        this.target = target;
        this.origin = origin;
    }

    public override NodeState Evaluate()
    {
        Vector3 tempTarget = new Vector3(target.position.x, origin.position.y, target.position.z);
        float distance = Vector3.Distance(tempTarget, origin.position);
        
        return distance <= range ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
