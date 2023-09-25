using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using CodeMonkey.HealthSystemCM;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private enum EnemyType
    {
        Idle,
        IdleAndChase,
        Chasing
    }
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private Transform enemyVisual;
    [SerializeField] private ParticleSystem getHitVFX;
    [SerializeField] private Transform deadVisual;

    private Vector3 playerPosition;
    private NavMeshAgent navMeshAgent;
    private HealthSystemComponent healthSystem;
    private bool canSeePlayer;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        healthSystem = GetComponent<HealthSystemComponent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        healthSystem.GetHealthSystem().OnDamaged += HealthSystem_OnDamaged;
        healthSystem.GetHealthSystem().OnDead += HealthSystem_OnDead;

        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;


    }

    private void HealthSystem_OnDead(object sender, EventArgs e)
    {
        Instantiate(deadVisual, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void HealthSystem_OnDamaged(object sender, EventArgs e)
    {
        getHitVFX.Play();
    }

    private void Update()
    {
        PlayerController player = FindAnyObjectByType<PlayerController>();
        if (!player) return;

        playerPosition = player.transform.position;
        Flip();

        switch (enemyType)
        {
            case EnemyType.Idle:
                break;
            case EnemyType.IdleAndChase:
                if (!canSeePlayer)
                {
                    navMeshAgent.isStopped = false;
                    navMeshAgent.SetDestination(playerPosition);
                }
                else
                {
                    navMeshAgent.isStopped = true;
                }
                // Nothing
                break;
            case EnemyType.Chasing:
                navMeshAgent.SetDestination(playerPosition);
                break;
            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        IsPlayerVisible();
    }

    public void Flip()
    {
        if (playerPosition.x < transform.position.x)
        {
            enemyVisual.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            enemyVisual.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public bool IsFlip()
    {
        return playerPosition.x < transform.position.x;
    }

    public Vector3 GetPlayerPosition()
    {
        return playerPosition;
    }

    public bool IsPlayerVisible()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerPosition - transform.position);
        if (hit.collider != null)
        {
            canSeePlayer = hit.collider.gameObject.GetComponent<PlayerController>();
            if (canSeePlayer)
            {
                Debug.DrawRay(transform.position, playerPosition - transform.position, Color.green);
            }
            else
            {
                Debug.DrawRay(transform.position, playerPosition - transform.position, Color.red);
            }
        }
        return canSeePlayer;
    }
}
