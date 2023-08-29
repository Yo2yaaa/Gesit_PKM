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


    void Start()
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

        Vector3 weaponPosition = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - weaponPosition.x;
        mousePos.y = mousePos.y - weaponPosition.y;

        float gunAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
        {
            // transform.rotation = Quaternion.Euler(new Vector3(180f, 0f, 0f));
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            // transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            transform.localScale = new Vector3(1, 1, 1);

        }
    }
}
