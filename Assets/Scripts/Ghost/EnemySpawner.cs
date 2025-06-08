using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    private GameObject playerObj;

    public float detectionRange = 10f;

    public float resetTime = 40f;

    private bool enemySpawned = false; 

    void Start()
    {
        playerObj = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        float distance = Vector3.Distance(playerObj.transform.position, this.transform.position);

        if (distance <= detectionRange && !enemySpawned)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, transform.rotation);
        enemySpawned = true;
        Invoke("Reset", resetTime);
    }

    private void Reset()
    {
        enemySpawned = false;
    }
}
