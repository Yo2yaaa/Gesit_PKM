using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] private Transform weapon;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float firingRate = 1f;

    [SerializeField] private float bulletForce = 20f;
    private Coroutine firingCoroutine;
    private Vector2 target;

    void Update()
    {
        RotateToTarget();
        GetInput();
    }

    private void GetInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(Shoot());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    private void RotateToTarget()
    {
        Vector2 mousePos = Input.mousePosition;

        target = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x -= target.x;
        mousePos.y -= target.y;

        float gunAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
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
