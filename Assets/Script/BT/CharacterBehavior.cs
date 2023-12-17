using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehavior : MonoBehaviour
{
    private BTNode _rootNode;

    private void Start()
    {
        // Xây d?ng Behavior Tree
        var actionNode = new BTActionNode(DoSomething);
        var sequenceNode = new BTSequenceNode();
        sequenceNode.AddChild(actionNode);

        _rootNode = sequenceNode;
    }

    private void Update()
    {
        // ?ánh giá Behavior Tree trong Update loop
        _rootNode.Evaluate();
    }

    private BTNodeState DoSomething()
    {
        // Hành ??ng c?a nút lá
        Debug.Log("Character is doing something...");

        return BTNodeState.Success;
    }
}
