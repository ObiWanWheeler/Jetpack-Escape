using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera cam = null;
    private NavMeshAgent agent;
    [SerializeField] private float packSpeed = 25f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameEvents.current.onPackFilled += UsePack;
    }

    private void Update()
    {        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Move our player              
                agent.SetDestination(hit.point);
            }  
        }
    }

    private void UsePack()
    {
        agent.speed = packSpeed;
    }
}
