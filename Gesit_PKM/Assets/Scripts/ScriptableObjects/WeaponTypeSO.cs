using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class WeaponTypeSO : ScriptableObject
{
    public WeaponRangeType weaponRangeType;
    public Transform bulletPrefab;
    public float timeBetweenAttack;
    public float damage;
    public float bulletMoveSpeed;
}

public enum WeaponRangeType
{
    closeRange,
    longRange
}

