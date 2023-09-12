using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private EnemyCloseRangeWeapon enemyCloseRangeWeapon;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwingWeapon()
    {
        animator.SetTrigger("Swing");
    }

    public void SetColliderActive()
    {
        enemyCloseRangeWeapon.SetActiveWeaponCollider(true);
    }

    public void SetColliderNonActive()
    {
        enemyCloseRangeWeapon.SetActiveWeaponCollider(false);
    }
}
