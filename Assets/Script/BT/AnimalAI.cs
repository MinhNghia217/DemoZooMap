using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAI : MonoBehaviour
{
    public Transform rootTranform;
    private Node topNode;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        //rootTranform = transform;
        ConstructBehahaviourTree();
    }

    private void ConstructBehahaviourTree()
    {
        ChaseNode chaseNode = new ChaseNode(player, this);
        RangeNode chasingRangeNode = new RangeNode(10f, player, transform);

        Sequence chaseSequence = new Sequence(new List<Node> { chasingRangeNode, chaseNode });

        ChaseNode GobackNode = new ChaseNode(rootTranform, this);
       // Inverter inverterRangeNode = new Inverter(chasingRangeNode);

       // Selector gobackSequence = new Selector(new List<Node> { inverterRangeNode, GobackNode });

        topNode = new Selector(new List<Node> { chaseSequence, GobackNode });
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
