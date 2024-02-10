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
    //--- PUBLIC INSPECTOR
    public NavMeshAgent MyNavMesh;
    public Transform Target;
    public Transform player;
    public List<Transform> TargetsMove = new List<Transform>();
    public float rangerFollowPlayer;
    public Animator animator;
    //--- USING LOCAL
    public NodeBase BehaviorTree { get; set; }
    private Coroutine m_BehaviorTreeRoutine;
    private YieldInstruction m_WaitTime = new WaitForSeconds(.1f);


    //--- CONFIG CONTROLLER
    private ConfigAnimal configAnimal;
    public PointInteract pointInteract;
    private void Start()
    {
        //Get Component
        MyNavMesh = GetComponent<NavMeshAgent>();
        configAnimal = GetComponent<ConfigAnimal>();
       

        //Init Function
        GenerateBehaviorTree();
        pointInteract.OnEnterTrigger += PointInteract_OnEnterTrigger;
        pointInteract.OnExitTrigger += PointInteract_OnExitTrigger;

        //Other
        if (m_BehaviorTreeRoutine == null && BehaviorTree != null)
        {
            m_BehaviorTreeRoutine = StartCoroutine(RunBehaviorTree());
        }
    }

    


    #region SET UP NODE FOR BEHAVIOR TREE
    private void GenerateBehaviorTree()
    {
        BehaviorTree = new Selector("ROOT ANIMAL",
         //MOVE TO PLAYER IF IN RANGE || GO AROUND 
        /* new Sequence("MOVE TO PLAYER",
                    new InRangeNode(rangerFollowPlayer, this, player),
                    new GoToTargetNode( MyNavMesh, player)),
            new GoAroundNode(TargetsMove, this)*/


            // COMTO PLAYER -> IDLE ANIMATION
            new Sequence("COME TO PLAYER",
                new CheckPointNode(configAnimal.CanInteract),
                new Selector("MOVE",
                    new Sequence("CHECK ARRIVED",
                        new GoToTargetNode(MyNavMesh, TargetsMove[0]),
                            new AnimationNode(animator,"idle")),
                    new AnimationNode(animator, "walk_forward"))));
    }
    #endregion

    #region Event Action
    private void PointInteract_OnExitTrigger()
    {
        configAnimal.CanInteract = false;
    }

    private void PointInteract_OnEnterTrigger()
    {
        configAnimal.CanInteract = true;
    }
    #endregion

    #region Default Behavior Tree
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
    #endregion

    #region DrawRanger
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangerFollowPlayer);
    }
    #endregion

    #region Template
    #endregion

}