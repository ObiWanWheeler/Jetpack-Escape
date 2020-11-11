using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

//used only on watching enemy types
public class SearchingBehaviour : StateMachineBehaviour
{
    #region variables
    [SerializeField] private float maxLookAngle = 90f;

    private Enemy enemy;
    private Transform enemyTransform;
    #endregion
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Initialising enemy
        enemy = animator.gameObject.GetComponent<Enemy>();
        //used to shorten code length
        enemyTransform = enemy.enemyObj.transform;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetBool("returned"))
        {
            RotateEnemy();

            bool foundEnemy = ShootSearchLight();

            if (foundEnemy)
            {
                animator.SetBool("isChasing", true);
            }
        }             
    }

    private void RotateEnemy()
    {
        float angle = Mathf.Sin(Time.time) * (maxLookAngle / (enemy.GetRotationSpeed() * 2));
        Vector3 rotation = new Vector3(0, angle, 0);
        enemyTransform.Rotate(rotation * enemy.GetRotationSpeed() * Time.deltaTime, Space.World);
    }

    private bool ShootSearchLight()
    {
        RaycastHit hitInfo;
        bool didHit = Physics.Raycast(enemyTransform.position, enemyTransform.right, out hitInfo, this.enemy.GetMaxDistance());
  
        if (didHit)
        {
            Debug.DrawLine(enemyTransform.position, hitInfo.point, Color.red);
            return hitInfo.collider.CompareTag("Player");
        }
        else
        {
            Debug.DrawLine(enemyTransform.position, enemyTransform.position + enemyTransform.right * this.enemy.GetMaxDistance(), Color.blue);
            return false;
        }
    }
}
