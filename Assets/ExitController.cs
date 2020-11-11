using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitController : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;

    [SerializeField] private float exitTime = 2f;
    private float timeLeft;
    private bool exited;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();

        exited = false;
        GameEvents.current.onPackFilled += Open;
    }

    private void Open()
    {
        meshRenderer.enabled = true;
        boxCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timeLeft = exitTime;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (exited == false)
            {
                if (timeLeft <= 0)
                {
                    GameEvents.current.LevelComplete();
                    exited = true;
                }
                else
                {
                    timeLeft -= Time.deltaTime;
                }
            }           
        }
    }
}
