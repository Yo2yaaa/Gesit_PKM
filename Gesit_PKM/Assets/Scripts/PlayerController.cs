using System;
using System.Collections;
using System.Collections.Generic;
using CodeMonkey.HealthSystemCM;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using MoreMountains.Feedbacks;
using System.Linq;
using UnityEngine.InputSystem;
using _Joytstick.Scripts;

public class PlayerController : MonoBehaviour
{
    public event Action<Vector2> OnEnemyInSight;
    public event Action<bool> OnMove;
    public event Action OnEnemyNotInSight;

    //Movement
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Transform playerVisual;
    [SerializeField] private ParticleSystem getHitVFX;
    [SerializeField] private Transform deadVisual;
    [SerializeField] private float viewRadius;
    [SerializeField] private LayerMask targetRaycastLayerMask;
    [SerializeField] private MMFeedbacks getHitFeedbacks;

    private Vector2 inputVector;
    private Rigidbody2D rb;
    private HealthSystemComponent healthSystem;
    private Transform closestTarget;
    private bool isWalking;
    private GameInput gameInput;
    private bool isAbleToMove = true;

    void Awake()
    {
        gameInput = GetComponent<GameInput>();
        rb = GetComponent<Rigidbody2D>();
        healthSystem = GetComponent<HealthSystemComponent>();
    }

    private void Start()
    {
        healthSystem.GetHealthSystem().OnDamaged += HealthSystem_OnDamaged;
        healthSystem.GetHealthSystem().OnDead += HealthSystem_OnDead;
    }

    private void HealthSystem_OnDead(object sender, EventArgs e)
    {
        Instantiate(deadVisual, transform.position, Quaternion.identity);
        GameManager.Instance.Lose();
        Destroy(gameObject);
    }

    private void HealthSystem_OnDamaged(object sender, EventArgs e)
    {
        getHitVFX.Play();
        getHitFeedbacks.PlayFeedbacks();
    }

    void Update()
    {
        inputVector = gameInput.GetMovementVectorNormalized();
        isWalking = inputVector != Vector2.zero;

        //Movement
        if (inputVector == Vector2.zero)
        {
            OnMove?.Invoke(false);
        }
        else
        {
            OnMove?.Invoke(true);
        }

        Flip();
        FindClosestTarget();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (!isAbleToMove) return;

        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * inputVector);
        // transform.position += moveSpeed * Time.fixedDeltaTime * (Vector3)inputVector;


    }

    public void Flip()
    {
        if (!closestTarget) return;

        if (IsFlip())
        {
            playerVisual.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            playerVisual.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void FindClosestTarget()
    {
        Enemy[] enemies = GameManager.Instance.GetEnemyList();
        if (enemies == null) return;

        float maxDistance = Mathf.Infinity;

        if (enemies.Length <= 0)
        {
            OnEnemyNotInSight?.Invoke();
            return;
        }

        foreach (Enemy enemy in enemies)
        {
            if (!enemy) return;

            float distanceBetween = Vector3.Distance(transform.position, enemy.transform.position);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, enemy.transform.position - transform.position, maxDistance, targetRaycastLayerMask);

            if (distanceBetween < maxDistance && hit.collider.gameObject.GetComponent<Enemy>())
            {
                closestTarget = enemy.transform;
                maxDistance = distanceBetween;
                OnEnemyInSight?.Invoke(closestTarget.position);
                return;
            }
            // if (distanceBetween < maxDistance)
            // {
            //     if (hit.collider.gameObject.GetComponent<Enemy>())
            //     {
            //         closestTarget = enemy.transform;
            //         maxDistance = distanceBetween;

            //         Debug.DrawRay(transform.position, enemy.transform.position - transform.position, Color.yellow);
            //     }
            //     else
            //     {
            //         OnEnemyNotInSight?.Invoke();

            //         Debug.DrawRay(transform.position, enemy.transform.position - transform.position, Color.black);
            //     }
            // }
        }
        OnEnemyNotInSight?.Invoke();
    }

    public bool IsFlip()
    {
        return closestTarget.position.x < transform.position.x;
    }

    public void SetActiveMovement(bool status)
    {
        isAbleToMove = status;
    }
}
