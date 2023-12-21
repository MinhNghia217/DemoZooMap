using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public float ranger = 20f;
    public Transform rootTranform;
    private Node topNode;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //rootTranform = transform;
        ConstructBehahaviourTree();
    }

    private void ConstructBehahaviourTree()
    {
        ChaseNode chaseNode = new ChaseNode(player, this, agent);
        RangeNode chasingRangeNode = new RangeNode(ranger, player, transform);
        RotationNode rotationNode = new RotationNode(this,player);
        ChaseNode GobackNode = new ChaseNode(rootTranform, this, agent);


        Sequence chaseSequence = new Sequence(new List<Node> { chasingRangeNode, rotationNode, chaseNode });
        Selector GoBack = new Selector(new List<Node> { GobackNode, rotationNode });
     

        topNode = new Selector(new List<Node> { chaseSequence, GoBack });
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
