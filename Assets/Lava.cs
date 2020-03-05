using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public float Speed = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }else if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Hurt(999);
        }
    }

    private void Update()
    {
        transform.Translate(0, Speed * Time.deltaTime, 0);
        Speed += Time.deltaTime * 0.01f;
    }
}
