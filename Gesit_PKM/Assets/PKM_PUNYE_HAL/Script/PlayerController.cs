using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movement
    [SerializeField] float LRSpeed = 3f;
    [SerializeField] float FBSpeed = 3f;
    
    [SerializeField] Main main;
    
    bool facingRight = true; 
    private Animator myAnim;


    void Start()
    {
        //Animasi
        myAnim = GetComponent<Animator>();
    }

    void Update()
    {
        //Movement
        float LRSpeedMove = Input.GetAxis("Horizontal") * LRSpeed * Time.deltaTime;
        float FBSpeedMove = Input.GetAxis("Vertical") * FBSpeed * Time.deltaTime;
        transform.Translate(LRSpeedMove, FBSpeedMove, 0);

        //Attacking
        if (Input.GetKeyDown(KeyCode.Space)){
            myAnim.Play("Attack");
        }
    
        // Flip + Animasi Run
        if (LRSpeedMove != 0 || FBSpeedMove != 0){
            myAnim.Play("Run");
            if (LRSpeedMove<0 && facingRight){
                flip();
            }
            else if (LRSpeedMove>0 && !facingRight){
                flip();
            }
        }

        //Idle (Diam)
        if (Input.GetKeyUp(KeyCode.Space)){
            myAnim.Play("Idle");
        }

    }

    private void OnTriggerEnter2D(Collider2D Collider)
    {
        if (Collider.tag == "Finish")
        {
            main.Next();
        }
    }

    void flip(){
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
}
