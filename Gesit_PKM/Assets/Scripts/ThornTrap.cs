using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.HealthSystemCM;

public class ThornTrap : MonoBehaviour
{
    [SerializeField] private float damage = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (HealthSystem.TryGetHealthSystem(other.gameObject, out HealthSystem healthSystem, true))
        {
            healthSystem.Damage(damage);
        }
    }
}
