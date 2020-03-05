using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform PlayerTransform;
    public float spawnCooldown;
    private float _currentSpawnCooldown;
    public Enemy enemyInstance;

    public Transform[] spawnPoints;

    private Transform _spawners;
    // Start is called before the first frame update
    void Start()
    {
        _spawners = transform.Find("Spawners");
    }

    // Update is called once per frame
    void Update()
    {
        _currentSpawnCooldown -= Time.deltaTime;
        if (_currentSpawnCooldown <= 0f)
        {
            Instantiate(enemyInstance, spawnPoints[Random.Range(0,spawnPoints.Length)].position, Quaternion.identity, transform);
            _currentSpawnCooldown = spawnCooldown;
        }
    }

    public void OnLevelUp()
    {
        _spawners.position = new Vector3(0, PlayerTransform.position.y, PlayerTransform.position.z);
        if (spawnCooldown > 0.3f)
        {
            spawnCooldown *= 0.90f;
        }
    }
}
