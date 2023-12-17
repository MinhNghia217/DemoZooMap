using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTActionNode : BTNode
{
    private readonly Func<BTNodeState> _action;

    public BTActionNode(Func<BTNodeState> action)
    {
        _action = action;
    }

    public override BTNodeState Evaluate()
    {
        return _action.Invoke();
    }
}
