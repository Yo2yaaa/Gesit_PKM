using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour{

    [SerializeField] float speed;
    [SerializeField] float maxRange;
    [SerializeField] Transform Home;
    
    private Animator myAnim;
    private Transform target;
    bool facingRight = true;

    public StatBarScript statBarScript;
    public WeaponHit weaponHitScript;
    public ArmorStatBarScript armorStatBarScript;

    private void Start(){
        myAnim = GetComponent<Animator>();
        target = FindObjectOfType<PlayerController>().transform;
    }

    private void Update(){
        float DistancePlayerEnemies = target.transform.position.x - transform.position.x;
        float DistanceHomeEnemies = Home.transform.position.x - transform.position.x;
    
        //Range + Flip + Animasi
        if(Vector3.Distance(target.position, transform.position) <= maxRange){
            FollowPlayer();
            //Check For Enemie Flip
            if (DistancePlayerEnemies<0 && facingRight){
                Flip();
            }
            if (DistancePlayerEnemies>0 && !facingRight){
                Flip();
            }
        }
        else if (transform.position != Home.transform.position && Vector3.Distance(target.position, transform.position) >= maxRange){
            GoHome();
            if (DistanceHomeEnemies<0 && facingRight){
                Flip();
            }
            if (DistanceHomeEnemies>0 && !facingRight){
                Flip();
            }
        }
        else{
            myAnim.Play("Idle");
        }

        // else d atas = if d bwh
        // if (transform.position == Home.transform.position){
        //     myAnim.Play("Idle");
        // }
    }

    public void FollowPlayer(){
        myAnim.Play("Run");
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    public void GoHome(){
        myAnim.Play("Run");
        transform.position =Vector3.MoveTowards(transform.position, Home.transform.position, speed * Time.deltaTime);

    }

    //Enemies Flip
    void Flip(){
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    void OnTriggerEnter2D(Collider2D Collider){
        if (Collider.tag == "Weapon"){
            weaponHitScript.EnemyCounter();
            Destroy(gameObject);            //Satu baris yg sangat berpengaruh pada StatBar & ArmorStatBar
        }
        else{
            if (statBarScript.enabled = true && Collider.tag == "Player"){
                statBarScript.DamageToStatBar(2);
                Destroy(gameObject);
                Debug.Log("Kena Serang!!!");
            }
            else if (armorStatBarScript.enabled = true && Collider.tag == "Armor"){
                armorStatBarScript.DamageToArmorStatBar(1);
                Destroy(gameObject);
                Debug.Log("Kena Armor!!!");
            }
        }
    }
}
