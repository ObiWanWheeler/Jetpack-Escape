using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    #region variables
    [SerializeField] private float enemySpeed = 10f;
    [SerializeField] private float maxLookDistance = 20f;

    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public GameObject enemyObj;

    private Quaternion startRotation;
    [SerializeField] private float rotationSpeed = 10;
    #endregion
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyObj = gameObject;
        startRotation = transform.rotation;
    }

    #region helper functions
    public float GetSpeed()
    {
        return enemySpeed;
    }

    public Quaternion GetStartRotation()
    {
        return startRotation;
    }

    public float GetRotationSpeed()
    {
        return rotationSpeed;
    }

    public float GetMaxDistance()
    {
        return maxLookDistance;
    }
    #endregion
}
