using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationNode : Node
{
    private AnimalAI ai;
    private Transform target;

    private Vector3 currentVelocity;
    private float smoothDamp=1f;
    
    public RotationNode(AnimalAI ai, Transform target )
    {
        this.ai = ai;
        this.target = target;
    }
    public override NodeState Evaluate()
    {
        Vector3 direction = target.position - ai.transform.position;
        Vector3 currentDirection = Vector3.SmoothDamp(ai.transform.forward, direction, ref currentVelocity, smoothDamp);
        Quaternion rotation = Quaternion.LookRotation(currentDirection, Vector3.up);
        ai.transform.rotation = rotation;
        return NodeState.RUNNING;
    }
}
