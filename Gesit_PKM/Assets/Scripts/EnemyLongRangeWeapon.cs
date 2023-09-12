using System.Collections;
using System.Collections.Generic;
using CodeMonkey.HealthSystemCM;
using UnityEngine;

public class EnemyLongRangeWeapon : MonoBehaviour
{
    [SerializeField] private WeaponTypeSO weaponTypeSO;
    [SerializeField] private Transform weapon;
    [SerializeField] private Transform shootPoint;

    private Enemy enemy;
    private Vector2 target;

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
        Vector2 playerPosition = enemy.GetPlayerPosition();

        target = transform.position;
        playerPosition.x -= target.x;
        playerPosition.y -= target.y;

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
            Transform bulletTransform = Instantiate(weaponTypeSO.bulletPrefab, shootPoint.position, shootPoint.rotation);
            Vector3 shootDirection = (shootPoint.transform.position - weapon.transform.position).normalized;
            bulletTransform.TryGetComponent(out Bullet bullet);
            bullet.Setup(shootDirection, weaponTypeSO.damage, weaponTypeSO.bulletMoveSpeed);

            yield return new WaitForSeconds(weaponTypeSO.timeBetweenAttack);
        }

    }
}
