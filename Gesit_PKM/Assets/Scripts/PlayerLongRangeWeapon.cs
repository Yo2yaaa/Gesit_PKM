using System;
using System.Collections;
using System.Collections.Generic;
using CodeMonkey.HealthSystemCM;
using Unity.VisualScripting;
using UnityEngine;
using MoreMountains.Feedbacks;

public class PlayerLongRangeWeapon : MonoBehaviour
{
    [SerializeField] private WeaponTypeSO weaponTypeSO;
    [SerializeField] private Transform weapon;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private MMFeedbacks shootingSFX;
    [SerializeField] private MMFeedbacks weaponAppearFeedback;
    [SerializeField] private MMFeedbacks weaponDisappearFeedback;

    private Coroutine firingCoroutine;
    private Vector2 target;
    private PlayerController playerController;
    private Vector2 targetDirection;
    private bool isEnemyInSight;
    private bool isPlayerMove;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        playerController.OnEnemyInSight += PlayerController_OnEnemyInSight;
        playerController.OnEnemyNotInSight += PlayerController_OnEnemyNotInSight;
        playerController.OnMove += PlayerController_OnMove;
    }

    private void PlayerController_OnMove(bool isPlayerMove)
    {
        this.isPlayerMove = isPlayerMove;
        if (isPlayerMove)
        {
            weaponDisappearFeedback.PlayFeedbacks();
        }
        else
        {
            weaponAppearFeedback.PlayFeedbacks();
        }
    }

    private void PlayerController_OnEnemyInSight(Vector2 targetPosition)
    {
        if (isPlayerMove)
        {
            if (firingCoroutine != null) StopCoroutine(firingCoroutine);
            isEnemyInSight = false;
            return;
        }

        RotateToTarget(targetPosition);

        if (!isEnemyInSight)
        {
            firingCoroutine = StartCoroutine(Shoot());
            isEnemyInSight = true;
        }
    }

    private void PlayerController_OnEnemyNotInSight()
    {
        if (firingCoroutine == null) return;
        StopCoroutine(firingCoroutine);
        isEnemyInSight = false;
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

            // SoundManager.Instance.PlayShootingSound();
            shootingSFX.PlayFeedbacks();

            float minNumber = weaponTypeSO.minTimeBetweenAttack;
            float maxNumber = weaponTypeSO.maxTimeBetweenAttack;
            float timeBetweenAttack = UnityEngine.Random.Range(minNumber, maxNumber);

            yield return new WaitForSeconds(timeBetweenAttack);
        }
    }
}
