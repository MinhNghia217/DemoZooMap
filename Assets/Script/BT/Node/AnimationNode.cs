using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationNode : Node
{
    private AnimalAI animalAI;
    private Animator animator;
    public AnimationNode(AnimalAI animalAI, Animator animator)
    {
        this.animalAI = animalAI;
        this.animator = animator;
    }
    
    public override NodeState Evaluate()
    {
        if (animalAI.state == STATE.run)
        {
            animator.SetBool("walk", true);
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}
