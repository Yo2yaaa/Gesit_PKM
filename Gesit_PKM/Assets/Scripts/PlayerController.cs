using System;
using System.Collections;
using System.Collections.Generic;
using CodeMonkey.HealthSystemCM;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    //Movement
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Transform playerVisual;
    [SerializeField] private ParticleSystem getHitVFX;
    [SerializeField] private Transform deadVisual;

    private float horizontalInput;
    private float verticalInput;
    private Rigidbody2D rb;
    private HealthSystemComponent healthSystem;
    private bool isDead;


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

        Flip();
    }

    private void FixedUpdate()
    {
        // rb.velocity = moveSpeed * Time.deltaTime * new Vector2(horizontalInput, verticalInput).normalized;
        rb.MovePosition(rb.position + new Vector2(horizontalInput, verticalInput) * moveSpeed * Time.fixedDeltaTime);
    }

    public void Flip()
    {
        Vector3 mousePos = Input.mousePosition;

        if (Camera.main.ScreenToWorldPoint(mousePos).x < transform.position.x)
        {
            playerVisual.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            playerVisual.transform.localScale = new Vector3(1, 1, 1);
        }
    }




}
