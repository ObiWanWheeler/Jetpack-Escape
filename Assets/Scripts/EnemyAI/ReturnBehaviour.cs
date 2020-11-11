using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using System;

public class ReturnBehaviour : StateMachineBehaviour
{
    #region variables
    private Enemy enemy;
    private Transform enemyTransform;
   
    private Transform startPoint;
    [SerializeField] private string startPointTag = "StartPoint";

    #endregion
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        #region initialising variables

        //Initialising enemy
        enemy = animator.gameObject.GetComponent<Enemy>();
        //used to shorten code length
        enemyTransform = enemy.enemyObj.transform;

        animator.SetBool("returned", false);

        //Initialising start point
        startPoint = FindStartPoint();
        Vector3 temp = new Vector3(startPoint.position.x, enemyTransform.position.y, startPoint.position.z);
        startPoint.transform.position = temp;
        #endregion

        //Return to start point
        MoveToStartPoint();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetBool("returned") == false)
        {
            #region checking transform
            //Checks to see if the enemy is currently stationed at his watch point
            bool atStartPoint = Mathf.Abs(Vector3.Distance(enemyTransform.position, startPoint.position)) <= 0.5f;

            if (atStartPoint == false)
            {
                //if he isn't allow him to keep moving there
                return;
            }

            //Checks to see if he is correctly rotated
            bool correctRotation = enemyTransform.rotation.eulerAngles == enemy.GetStartRotation().eulerAngles;

            if (correctRotation == false)
            {
                //if he isn't rotate him
                enemyTransform.rotation = Quaternion.Lerp(enemyTransform.rotation, enemy.GetStartRotation(), Time.deltaTime * enemy.GetRotationSpeed());
                return;
            }
            #endregion

            animator.SetBool("returned", true);
        }   
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("returned", false);
    }

    private Transform FindStartPoint()
    {
        Transform parent = enemyTransform.parent;
        int count = parent.childCount;

        #region iterating through children
        for (int i = 0; i < count; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.CompareTag(startPointTag))
                return child;
        }
        #endregion

        Debug.Log($"No start point could be found for {enemy.enemyObj.name}");

        return null;
    }

    private void MoveToStartPoint()
    {
        enemy.agent.SetDestination(startPoint.position);
    }
}
