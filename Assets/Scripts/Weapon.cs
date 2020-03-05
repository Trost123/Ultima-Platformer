using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float maxAttackCooldown;

    public float damage;
    public WeaponType weaponType;
    protected float _attackCooldown;

    public enum WeaponType
    {
        Unarmed,
        Melee,
        Ranged
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        _attackCooldown = maxAttackCooldown;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        _attackCooldown += Time.deltaTime;
    }

    public virtual void Init()
    {
    }

    public virtual void Attack()
    {
    }
}
