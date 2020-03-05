using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 10;
    public float health = 100;
    private Animator _animator;

    private Rigidbody _rigidbody;
    private BoxCollider _boxCollider;
    private Player _player;
    private bool _facingRight = true;

    private Player victim;

    // Start is called before the first frame update
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _player = Player.Instance;
        
        if (Math.Abs(transform.rotation.y) > 0.1f)
        {
            _facingRight = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_player)
        {
            float distanceToPlayerX = Math.Abs(_player.transform.position.x - transform.position.x);
            bool avoidCloseRange = _player.currentWeapon && (_player.currentWeapon.weaponType == Weapon.WeaponType.Melee && (_player.facingRight != this._facingRight)); //if player has a sword and looks at enemy
            if ((distanceToPlayerX > 4f || !avoidCloseRange) && Math.Abs(_rigidbody.velocity.x) < moveSpeed) //try to avoid sword range, approach if player not looking
            {
                _rigidbody.AddForce(transform.right * (moveSpeed * Time.deltaTime * 200f));
            }else if (distanceToPlayerX < 4f) //step back, the player is armed with a sword
            {
                _rigidbody.AddForce(-transform.right * (moveSpeed * Time.deltaTime * 100f));
            }

            bool playerIsOnLeft = _player.transform.position.x < this.transform.position.x;
            if (playerIsOnLeft && this._facingRight)
            {
                transform.Rotate(Vector3.up, 180f);
                _facingRight = false;
            }
            else if (!playerIsOnLeft && !this._facingRight)
            {
                transform.Rotate(Vector3.up, 180f);
                _facingRight = true;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        _rigidbody.AddForce((transform.up * 50f)+(transform.right*-300f));
        
        if (health <= 0)
        {
            Die();
        }
    }
    
    void Die()
    {
        _animator.SetTrigger("Die");
        _boxCollider.enabled = false;
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            victim = other.GetComponent<Player>();
            victim.Hurt(1);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && victim)
        {
            victim.Hurt(1);
        }
    }
}
