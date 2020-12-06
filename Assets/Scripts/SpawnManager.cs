﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    // Prefab for the enemy
    [SerializeField]
    GameObject _enemyPrefab;

    // Prefab for the enemy
    [SerializeField]
    GameObject _powerUpPrefab;

    [SerializeField]
    float _yBound = 6.7f;

    [SerializeField]
    float _xBound = 11.3f;

    Coroutine _spawnEnemies;
    Coroutine _spawnPowerup;

    // Start is called before the first frame update
    void Start()
    {
        _spawnEnemies = StartCoroutine(SpawnEnemiesRoutine());
        _spawnPowerup = StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // When the object is destroyed we ensure to stop coroutines
    void OnDestroy()
    {
        StopSpawning();
    }
 

    // Coroutine to spawn enemies
    IEnumerator SpawnEnemiesRoutine()
    {
        // Skip 1 frame
        yield return null;

        while (true)
        {
            // suspend execution for X seconds
            float waitTimeSec = Random.Range(1, 5f);
            yield return new WaitForSeconds(waitTimeSec);

            // Spawn the enemy
            // Random x Position
            float xPos = Random.Range(-_xBound, +_xBound);

            // Generate the enemy in the random position
            Instantiate(_enemyPrefab, new Vector3(xPos, _yBound, 0), Quaternion.identity, this.transform);

        }
    }


    // Coroutine to spawn powerups elements
    IEnumerator SpawnPowerUpRoutine()
    {
        // Skip 1 frame
        yield return null;

        while (true)
        {
            // suspend execution for X seconds
            float waitTimeSec = Random.Range(7f, 10f);
            yield return new WaitForSeconds(waitTimeSec);

            // Spawn the enemy
            // Random x Position
            float xPos = Random.Range(-_xBound, +_xBound);

            // Generate the enemy in the random position
            Instantiate(_powerUpPrefab, new Vector3(xPos, _yBound, 0), Quaternion.identity, this.transform);

        }
    }


    public void StopSpawning()
    {
        StopCoroutine(_spawnEnemies);
        StopCoroutine(_spawnPowerup);
    }

}
