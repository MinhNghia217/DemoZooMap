using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehavior : MonoBehaviour
{
    private BTNode _rootNode;

    private void Start()
    {
        // X�y d?ng Behavior Tree
        var actionNode = new BTActionNode(DoSomething);
        var sequenceNode = new BTSequenceNode();
        sequenceNode.AddChild(actionNode);

        _rootNode = sequenceNode;
    }

    private void Update()
    {
        // ?�nh gi� Behavior Tree trong Update loop
        _rootNode.Evaluate();
    }

    private BTNodeState DoSomething()
    {
        // H�nh ??ng c?a n�t l�
        Debug.Log("Character is doing something...");

        return BTNodeState.Success;
    }
}
