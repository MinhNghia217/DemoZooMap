using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum STATE
{
    idle,
    run
}

public class AnimalAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public float ranger = 20f;
    public Transform rootTranform;
    private Node topNode;
    public Transform player;

    public STATE state = STATE.idle;

    public List<Transform> targets =new List<Transform> ();
    public Animator animator;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //rootTranform = transform;
        ConstructBehahaviourTree();
    }

    private void ConstructBehahaviourTree()
    {
       // GoAroundNode goAround = new GoAroundNode(targets, agent, this);
        AnimationNode runNode = new AnimationNode(this, animator);
        //  RangeNode chasingRangeNode = new RangeNode(ranger, player, transform);
        //  RotationNode rotationNode = new RotationNode(this,player);
        //  ChaseNode GobackNode = new ChaseNode(rootTranform, this, agent);
        ChaseNode GoTargetNode_0 = new ChaseNode(targets[0], this, agent);
        ChaseNode GoTargetNode_1 = new ChaseNode(targets[1], this, agent);
        //  Sequence chaseSequence = new Sequence(new List<Node> { chasingRangeNode, rotationNode, chaseNode });
        Sequence chaseSequence = new Sequence(new List<Node> {  GoTargetNode_1, GoTargetNode_0 });
        //Selector GoBack = new Selector(new List<Node> { GobackNode, rotationNode });
     

        topNode = new Selector(new List<Node> { chaseSequence });
    }

 
    void Update()
    {

        topNode.Evaluate();
        if (topNode.nodeState == NodeState.FAILURE)
        {
            Debug.Log("ko lam gi");
        }

      //  Debug.Log(rootTranform.position);
    }
}
