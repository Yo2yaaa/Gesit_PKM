using System.Collections;
using System.Collections.Generic;
using CodeMonkey.HealthSystemCM;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 shootDirection;
    private float damage;
    private float moveSpeed;

    public void Setup(Vector3 shootDirection, float damage, float moveSpeed)
    {
        this.shootDirection = shootDirection;
        this.damage = damage;
        this.moveSpeed = moveSpeed;
        Destroy(gameObject, 4f);
    }

    private void Update()
    {
        transform.position += moveSpeed * Time.deltaTime * shootDirection;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (HealthSystem.TryGetHealthSystem(other.gameObject, out HealthSystem healthSystem, true))
        {
            healthSystem.Damage(damage);
        }
        Destroy(gameObject);
    }
}
