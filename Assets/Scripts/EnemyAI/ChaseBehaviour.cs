using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//Used on all enemy types
public class ChaseBehaviour : StateMachineBehaviour
{
    #region variables
    private Transform playerTransform;
    private Enemy enemy;
    #endregion
    public new void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        enemy = animator.gameObject.GetComponent<Enemy>();
    }

    public new void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemy.agent != null) 
            enemy.agent.SetDestination(playerTransform.position);
    }
}
