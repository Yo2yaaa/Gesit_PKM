using System;
using System.Collections;
using System.Collections.Generic;
using CodeMonkey.HealthSystemCM;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using MoreMountains.Feedbacks;
using System.Linq;

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

    private float horizontalInput;
    private float verticalInput;
    private Rigidbody2D rb;
    private HealthSystemComponent healthSystem;
    private Transform closestTarget;


    void Awake()
    {
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
        //Movement
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput == 0 && verticalInput == 0)
        {
            OnMove?.Invoke(false);
        }
        else
        {
            OnMove?.Invoke(true);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        // rb.velocity = moveSpeed * Time.deltaTime * new Vector2(horizontalInput, verticalInput).normalized;
        rb.MovePosition(rb.position + new Vector2(horizontalInput, verticalInput) * moveSpeed * Time.fixedDeltaTime);

        FindClosestTarget();
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
        float maxDistance = Mathf.Infinity;

        if (enemies.Length == 0)
        {
            OnEnemyNotInSight?.Invoke();
            return;
        }

        foreach (Enemy enemy in enemies)
        {
            float distanceBetween = Vector3.Distance(transform.position, enemy.transform.position);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, enemy.transform.position - transform.position, maxDistance, targetRaycastLayerMask);

            if (distanceBetween < maxDistance && hit.collider.gameObject.GetComponent<Enemy>())
            {
                closestTarget = enemy.transform;
                maxDistance = distanceBetween;
                OnEnemyInSight?.Invoke(closestTarget.position);

                Debug.DrawRay(transform.position, enemy.transform.position - transform.position, Color.yellow);
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
}
