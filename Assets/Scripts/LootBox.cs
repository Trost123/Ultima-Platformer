using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class LootBox : MonoBehaviour
{
    public Weapon.WeaponType loot;
    private static readonly int Pickup = Animator.StringToHash("Pickup");

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player>().SetWeapon(loot);
            this.GetComponent<BoxCollider>().enabled = false;
            GetComponent<Animator>().SetTrigger(Pickup);
            Destroy(gameObject, 1f);
        }
    }
}
