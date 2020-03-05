using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Gun : Weapon
{
    public float shootingRange;
    LineRenderer _lineRenderer;
    // Start is called before the first frame update
    protected override void Start()
    {
        weaponType = WeaponType.Ranged;
        _lineRenderer = GetComponentInChildren<LineRenderer>();
    }

    public override void Attack()
    {
        base.Attack();

        if (_attackCooldown > maxAttackCooldown)
        {
            Vector3 fwd = transform.TransformDirection(Vector3.right);

            RaycastHit hit;
            if (Physics.Raycast(_lineRenderer.transform.position, fwd, out hit, shootingRange))
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    hit.transform.GetComponent<Enemy>().TakeDamage(damage);
                }
            }

            Debug.DrawRay(_lineRenderer.transform.position, fwd * hit.distance, Color.green, 1f);
            if (hit.distance > 0f)
            {
                _lineRenderer.SetPosition(1,
                    Vector3.right * (hit.distance)); //0.5 because line renderer starts inside of gun
            }
            else
            {
                _lineRenderer.SetPosition(1,
                    Vector3.right * (shootingRange)); //0.5 because line renderer starts inside of gun
            }

            _lineRenderer.enabled = true;
            Invoke(nameof(DisableLineRenderer), 0.2f);
            _attackCooldown = 0;
        }
    }

    private void DisableLineRenderer()
    {
        _lineRenderer.enabled = false;
    }
}
