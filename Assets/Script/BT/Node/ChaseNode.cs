using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseNode : Node
{
    private AnimalAI AnimalAI;
    private Transform target;
    public ChaseNode(Transform targe, AnimalAI animalAI)
    {
        this.target = targe;
        AnimalAI = animalAI;
    }
    public override NodeState Evaluate()
    {
       
        float distance = Vector3.Distance(target.position, AnimalAI.transform.position);
        Debug.Log(distance+"/"+ target);
        if (distance > 0.2f)
        {
           MoveTowards(target.position);
            Debug.Log("chay lai ne"+ target);
            return NodeState.RUNNING;
        }
        else
        {
          
            return NodeState.SUCCESS;
        }
    }

    public void MoveTowards(Vector3 targetPosition)
    {
        // Tính toán h??ng di chuy?n t? v? trí hi?n t?i c?a AnimalAI ??n targetPosition
        Vector3 direction = (targetPosition - AnimalAI.transform.position).normalized;
       
        // Di chuy?n AnimalAI theo h??ng tính toán
        AnimalAI.transform.position += direction  * Time.deltaTime;
    }
}
