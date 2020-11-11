using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckingBehaviour : StateMachineBehaviour
{
    #region variables
    private Enemy enemy;
    private Vector3 enemyPos;
    [SerializeField] private int playerLayer = 9;
    #endregion
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.gameObject.GetComponent<Enemy>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyPos = enemy.enemyObj.transform.position;

        //check if he can / can no longer see the player
        animator.SetBool("isChasing", CanSeePlayer());
    }
    private bool CanSeePlayer()
    {
        CheckForPlayerInRange(out GameObject player);

        //if this is null , CheckForPlayerInRange wasn't able to find a player
        if (player == null)
        {
            return false;
        }

        return CheckIfVisible(player);
    }

    private void CheckForPlayerInRange(out GameObject player)
    {
        int layerMask = 1 << playerLayer;

        //shoots out a sphere that detects any object is the player layer
        Collider[] playerColliders = Physics.OverlapSphere(enemyPos, enemy.GetMaxDistance(), layerMask);

        //if the player is not in this range, there will be  no colliders in this array, as there is only one player
        if (playerColliders.Length == 0)
            player = null;
        //if for some reason, more than one player is detected
        else if (playerColliders.Length != 1)
        {
            Debug.Log("Multiple entities on 'Player' layer detected!");
            player = null;
        }
        else
            player = playerColliders[0].gameObject;
    }

    private bool CheckIfVisible(GameObject player)
    {
        Physics.Linecast(enemyPos, player.transform.position, out RaycastHit hitInfo);

        //if somehow the ray missed everything, it obviously didn't hit the player, so return false
        if (hitInfo.collider == null)
            return false;

        //if this ray hit the player, not a wall collider
        if (hitInfo.collider.CompareTag("Player"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
