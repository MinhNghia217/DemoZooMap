using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSequenceNode : BTNode
{
    private readonly List<BTNode> _childNodes = new List<BTNode>();

    public void AddChild(BTNode node)
    {
        _childNodes.Add(node);
    }

    public override BTNodeState Evaluate()
    {
        foreach (var childNode in _childNodes)
        {
            var result = childNode.Evaluate();

            if (result != BTNodeState.Success)
            {
                return result;
            }
        }

        return BTNodeState.Success;
    }
}
