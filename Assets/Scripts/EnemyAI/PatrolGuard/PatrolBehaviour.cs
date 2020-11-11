using UnityEngine;
using UnityEngine.AI;

//used only on patrolling enemy types
public class PatrolBehaviour : StateMachineBehaviour
{
    #region variables
    private Enemy enemy;
    private Transform enemyTransform;

    private Transform[] patrolPoints;
    private int currentPatrolPointIndex;

    private float waitTime;
    [SerializeField] private float startWaitTime = 2f;
    #endregion
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        #region initialising variables
        //Initialising enemy
        enemy = animator.gameObject.GetComponent<Enemy>();
        //used to shorten code length
        enemyTransform = enemy.enemyObj.transform;

        //initialising waypoints
        GetPatrolPoints();
        AllignPoints();
        waitTime = startWaitTime;
        currentPatrolPointIndex = 0;
        #endregion
    }


    private void GetPatrolPoints()
    {
        patrolPoints = enemy.gameObject.GetComponent<PatrolPoints>().patrolPoints;
    }

    private void AllignPoints()
    {
        foreach (var point in patrolPoints)
        {
            Vector3 temp = new Vector3(point.position.x, enemyTransform.position.y, point.position.z);
            point.position = temp;
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetBool("returned"))
        {
            //If enemy has reached it's next patrol point
            if (Mathf.Abs(Vector3.Distance(patrolPoints[currentPatrolPointIndex].position, enemyTransform.position)) <= 0.5f)
            {
                #region patrol point checks
                if (waitTime <= 0f)
                {
                    currentPatrolPointIndex++;

                    if (currentPatrolPointIndex >= patrolPoints.Length)
                    {
                        currentPatrolPointIndex = 0;
                    }

                    waitTime = startWaitTime;

                    //Set it off moving towards the next
                    enemy.agent.SetDestination(patrolPoints[currentPatrolPointIndex].position);
                }
                #endregion
                else
                    waitTime -= Time.deltaTime;
            }
        }
    }
}
