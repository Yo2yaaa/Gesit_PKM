using System.Collections;
using System.Collections.Generic;
using CodeMonkey.HealthSystemCM;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCloseRangeWeapon : MonoBehaviour
{
    [SerializeField] private WeaponTypeSO weaponTypeSO;
    [SerializeField] private Transform weapon;
    [SerializeField] private EnemyAnimation enemyAnimation;
    [SerializeField] private PolygonCollider2D weaponColldier;

    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        Vector2 playerPosition = enemy.GetPlayerPosition();
        if (Vector2.Distance(playerPosition, transform.position) < 3)
        {
            enemyAnimation.SwingWeapon();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (HealthSystem.TryGetHealthSystem(other.gameObject, out HealthSystem healthSystem, true))
        {
            healthSystem.Damage(weaponTypeSO.damage);
        }
    }

    public void SetActiveWeaponCollider(bool isActive)
    {
        weaponColldier.enabled = isActive;
    }
}
