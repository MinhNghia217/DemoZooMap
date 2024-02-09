using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using WUG.BehaviorTreeVisualizer;
using Node = WUG.BehaviorTreeVisualizer.Node;

public enum NavigationActivity
{
    goToTarget
}
public class AnimalController : MonoBehaviour, IBehaviorTree
{
    //---PARAMETE PUBLIC INSPECTOR
    public NavMeshAgent MyNavMesh;
    public Transform Target;
    public Transform player;
    public List<Transform> TargetsMove = new List<Transform>();
    public float rangerFollowPlayer;
    //--- USING LOCAL
    public NodeBase BehaviorTree { get; set; }
    private Coroutine m_BehaviorTreeRoutine;
    private YieldInstruction m_WaitTime = new WaitForSeconds(.1f);

    private void Start()
    {
        MyNavMesh = GetComponent<NavMeshAgent>();
        GenerateBehaviorTree();
        
        if (m_BehaviorTreeRoutine == null && BehaviorTree != null)
        {
            m_BehaviorTreeRoutine = StartCoroutine(RunBehaviorTree());
        }
    }

    private void GenerateBehaviorTree()
    {
        BehaviorTree = new Selector("ROOT ANIMAL",
         /*new GoToTargetNode(NavigationActivity.goToTarget, MyNavMesh, Target),
         new Sequence("MOVE TO PLAYER",
             new InRangeNode(5f,this,player),
             new GoToTargetNode(NavigationActivity.goToTarget, MyNavMesh, player))*/
         new Sequence("MOVE TO PLAYER",
                    new InRangeNode(rangerFollowPlayer, this, player),
                    new GoToTargetNode(NavigationActivity.goToTarget, MyNavMesh, player)),
            new GoAroundNode(TargetsMove,this)
            



       );                    
    }

    private IEnumerator RunBehaviorTree()
    {
        while (enabled)
        {
            if (BehaviorTree == null)
            {
                $"{this.GetType().Name} is missing Behavior Tree. Did you set the BehaviorTree property?".BTDebugLog();
                continue;
            }

            (BehaviorTree as Node).Run();

            yield return m_WaitTime;
        }
    }

    public void ForceDrawingOfTree()
    {
        if (BehaviorTree == null)
        {
            $"Behavior tree is null - nothing to draw.".BTDebugLog();
        }
        //Tell the tool to draw the referenced behavior tree. The 'true' parameter tells it to give focus to the window. 
        BehaviorTreeGraphWindow.DrawBehaviorTree(BehaviorTree, true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangerFollowPlayer);
    }
}