using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    //Script PlayerController ketika di hp, seperti pada zombero [Sementara Nonaktif] 

    private Animator myAnim;
    [SerializeField] float speed = 5f;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = transform.position.z;   
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        myAnim.Play("Run");
        }
    
        if (Input.GetMouseButtonUp(0)){
            myAnim.Play("Attack");
            // myAnim.Play("Idle");
        }

        // else{
        // }
    }

    
}
