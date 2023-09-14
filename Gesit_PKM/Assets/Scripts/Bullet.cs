using System.Collections;
using System.Collections.Generic;
using CodeMonkey.HealthSystemCM;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum Source
    {
        Player,
        Enemy
    }

    private Source source;
    private Vector3 shootDirection;
    private float damage;
    private float moveSpeed;

    public void Setup(Vector3 shootDirection, float damage, float moveSpeed, Source source)
    {
        this.shootDirection = shootDirection;
        this.damage = damage;
        this.moveSpeed = moveSpeed;
        this.source = source;
        Destroy(gameObject, 4f);
    }

    private void Update()
    {
        transform.position += moveSpeed * Time.deltaTime * shootDirection;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Enemy>() && source == Source.Enemy) return;
        if (other.GetComponent<PlayerController>() && source == Source.Player) return;

        if (HealthSystem.TryGetHealthSystem(other.gameObject, out HealthSystem healthSystem, true))
        {
            healthSystem.Damage(damage);
        }

        Destroy(gameObject);
    }
}
