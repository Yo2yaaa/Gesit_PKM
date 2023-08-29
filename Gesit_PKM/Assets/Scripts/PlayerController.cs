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
    private Rigidbody2D rb;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
