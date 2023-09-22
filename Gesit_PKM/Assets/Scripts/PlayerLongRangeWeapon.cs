using System;
using System.Collections;
using System.Collections.Generic;
using CodeMonkey.HealthSystemCM;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLongRangeWeapon : MonoBehaviour
{
    [SerializeField] private WeaponTypeSO weaponTypeSO;
    [SerializeField] private Transform weapon;
    [SerializeField] private Transform shootPoint;

    private Coroutine firingCoroutine;
    private Vector2 target;
    private PlayerController playerController;
    private Vector2 targetDirection;
    private bool isEnemyInSight;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        playerController.OnEnemyInSight += PlayerController_OnEnemyInSight;
        playerController.OnEnemyNotInSight += PlayerController_OnEnemyNotInSight;
    }

    private void PlayerController_OnEnemyInSight(Vector2 targetPosition)
    {
        RotateToTarget(targetPosition);

        if (!isEnemyInSight)
        {
            firingCoroutine = StartCoroutine(Shoot());
            isEnemyInSight = true;
        }
    }

    private void PlayerController_OnEnemyNotInSight()
    {
        StopCoroutine(firingCoroutine);
        isEnemyInSight = false;
    }

    void Update()
    {
        // RotateToTarget();
        // GetInput();
    }

    private void GetInput()
    {

        // if (Input.GetButtonDown("Fire1"))
        // {
        //     firingCoroutine = StartCoroutine(Shoot());
        // }

        // if (Input.GetButtonUp("Fire1"))
        // {
        //     StopCoroutine(firingCoroutine);
        // }
    }

    private void RotateToTarget(Vector2 targetPosition)
    {
        targetPosition.x -= transform.position.x;
        targetPosition.y -= transform.position.y;

        float gunAngle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;
        // weapon.transform.right = targetPosition.normalized;

        if (playerController.IsFlip())
        {
            weapon.transform.localScale = new Vector3(-1, 1, 1);
            weapon.transform.rotation = Quaternion.Euler(new Vector3(180f, 0f, -gunAngle));
        }
        else
        {
            weapon.transform.localScale = new Vector3(1, 1, 1);
            weapon.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, gunAngle));
        }
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            Transform bulletTransform = Instantiate(weaponTypeSO.bulletPrefab, shootPoint.position, shootPoint.rotation);
            Vector3 shootDirection = (shootPoint.transform.position - weapon.transform.position).normalized;
            bulletTransform.TryGetComponent(out Bullet bullet);
            bullet.Setup(shootDirection, weaponTypeSO.damage, weaponTypeSO.bulletMoveSpeed, Bullet.Source.Player);

            SoundManager.Instance.PlayShootingSound();

            yield return new WaitForSeconds(weaponTypeSO.timeBetweenAttack);
        }
    }
}
