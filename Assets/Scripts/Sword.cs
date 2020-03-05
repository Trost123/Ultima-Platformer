using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Sword : Weapon
{
    
    private Animator _animator;
    private List<Enemy> _currentEnemies = new List<Enemy>();
    
    // Start is called before the first frame update
    protected override void Start()
    {
        _animator = GetComponent<Animator>();
        weaponType = WeaponType.Melee;
    }
    
    public void DealCleaveDamage() //called inside sword animation
    {
        for (int i = _currentEnemies.Count-1; i >= 0; i--)
        {
            var anEnemy = _currentEnemies[i];
            anEnemy.TakeDamage(damage);
            if (anEnemy.health <= 0f)
            {
                _currentEnemies.Remove(anEnemy);
            }
        }
    }

    public override void Attack()
    {
        if (_attackCooldown > maxAttackCooldown)
        {
            _animator.SetTrigger("Attack");
            _attackCooldown = 0;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _currentEnemies.Add(other.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _currentEnemies.Remove(other.GetComponent<Enemy>());
    }
}
