using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    //Movement
    [SerializeField] private float moveSpeed = 3f;


    private float horizontalInput;
    private float verticalInput;
    private bool facingRight = true;
    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Movement
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        // rb.velocity = moveSpeed * Time.deltaTime * new Vector2(horizontalInput, verticalInput).normalized;
        rb.MovePosition(rb.position + new Vector2(horizontalInput, verticalInput) * moveSpeed * Time.fixedDeltaTime);
    }

    public bool IsFlip()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition.x < transform.position.x)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
