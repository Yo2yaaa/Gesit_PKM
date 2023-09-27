using System.Collections;
using System.Collections.Generic;
using CodeMonkey.HealthSystemCM;
using Unity.VisualScripting;
using UnityEngine;
using MoreMountains.Feedbacks;

public class EnemyLongRangeWeapon : MonoBehaviour
{
    [SerializeField] private WeaponTypeSO weaponTypeSO;
    [SerializeField] private Transform weapon;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private MMFeedbacks shootingSFX;

    private Enemy enemy;
    private Vector3 playerPosition = new(0, 0);
    private bool hasShootCoroutine;
    private Coroutine firingCoroutine;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
        RotateToTarget();
    }

    private void RotateToTarget()
    {
        playerPosition = enemy.GetPlayerPosition();

        playerPosition.x -= transform.position.x;
        playerPosition.y -= transform.position.y;

        float gunAngle = Mathf.Atan2(playerPosition.y, playerPosition.x) * Mathf.Rad2Deg;

        if (enemy.IsFlip())
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
            StartShooting();

            float minNumber = weaponTypeSO.minTimeBetweenAttack;
            float maxNumber = weaponTypeSO.maxTimeBetweenAttack;
            float timeBetweenAttack = Random.Range(minNumber, maxNumber);

            yield return new WaitForSeconds(timeBetweenAttack);
        }
    }

    private void StartShooting()
    {
        if (!enemy.IsPlayerVisible()) return;

        Transform bulletTransform = Instantiate(weaponTypeSO.bulletPrefab, shootPoint.position, shootPoint.rotation);
        Vector3 shootDirection = (shootPoint.transform.position - weapon.transform.position).normalized;
        _ = bulletTransform.TryGetComponent(out Bullet bullet);
        bullet.Setup(shootDirection, weaponTypeSO.damage, weaponTypeSO.bulletMoveSpeed, Bullet.Source.Enemy);

        shootingSFX.PlayFeedbacks();
    }
}
