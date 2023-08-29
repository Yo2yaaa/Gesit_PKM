using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{
    [SerializeField] private Transform weapon;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float firingRate = 1f;

    [SerializeField] private float bulletForce = 20f;

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

        if (enemy.isFlip())
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
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bulletPrefab.gameObject.SetActive(true);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
            Destroy(bullet, 3f);
            yield return new WaitForSeconds(firingRate);
        }
    }
}
