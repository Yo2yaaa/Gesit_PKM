using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;
    private Vector2 playerPosition;

    private Rigidbody2D rb;
    private NavMeshAgent navMeshAgent;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }

    private void Update()
    {
        playerPosition = FindAnyObjectByType<PlayerController>().transform.position;
        navMeshAgent.SetDestination(playerPosition);

        Flip();
    }

    public void Flip()
    {
        if (playerPosition.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public bool isFlip()
    {
        return playerPosition.x < transform.position.x;
    }

    public Vector2 GetPlayerPosition()
    {
        return playerPosition;
    }
}
