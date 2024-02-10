using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WUG.BehaviorTreeVisualizer;

public class AnimationNode : Node
{
    private Animator animator;
    private string nameAnimation;

    public AnimationNode(Animator animator, string nameAnimation)
    {
        this.animator = animator;
        this.nameAnimation = nameAnimation;
    }

    protected override void OnReset()
    {    }

    protected override NodeStatus OnRun()
    {

        animator.Play(nameAnimation);
        return NodeStatus.Success;
    }

}
