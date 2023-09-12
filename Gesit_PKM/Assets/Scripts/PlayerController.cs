using System;
using System.Collections;
using System.Collections.Generic;
using CodeMonkey.HealthSystemCM;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    public event Action<Vector2> OnEnemyInSight;
    public event Action OnEnemyNotInSight;

    //Movement
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Transform playerVisual;
    [SerializeField] private ParticleSystem getHitVFX;
    [SerializeField] private Transform deadVisual;
    [SerializeField] private float viewRadius;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;

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
        Time.timeScale = 0;
        Destroy(gameObject);
    }

    private void HealthSystem_OnDamaged(object sender, EventArgs e)
    {
        getHitVFX.Play();
    }

    void Update()
    {
        //Movement
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        FindClosestTarget();

        Flip();
    }

    private void FixedUpdate()
    {
        // rb.velocity = moveSpeed * Time.deltaTime * new Vector2(horizontalInput, verticalInput).normalized;
        rb.MovePosition(rb.position + new Vector2(horizontalInput, verticalInput) * moveSpeed * Time.fixedDeltaTime);
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
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        closestTarget = null;
        float maxDistance = Mathf.Infinity;

        if (enemies.Length == 0)
        {
            OnEnemyNotInSight?.Invoke();
            return;
        }

        foreach (Enemy enemy in enemies)
        {
            float distanceBetween = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceBetween < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = distanceBetween;
            }
        }
        OnEnemyInSight?.Invoke(closestTarget.position);
    }

    public bool IsFlip()
    {
        return closestTarget.position.x < transform.position.x;
    }
}
