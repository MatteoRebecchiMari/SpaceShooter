using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    // Prefab for the enemy
    [SerializeField]
    GameObject _enemyPrefab;

    [SerializeField]
    GameObject[] _powerUpPrefabs;

    [SerializeField]
    float _yBound = 6.7f;

    [SerializeField]
    float _xBound = 11.3f;

    Coroutine _spawnEnemies;
    Coroutine _spawnPowerup;

    // Start spawning enemies
    public void StartSpawning()
    {
        _spawnEnemies = StartCoroutine(SpawnEnemiesRoutine());
        _spawnPowerup = StartCoroutine(SpawnPowerUpRoutine());
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
            // suspend execution for X seconds i
            float waitTimeSec = Random.Range(3f, 7f);
            yield return new WaitForSeconds(waitTimeSec);

            // Spawn the enemy
            // Random x Position
            float xPos = Random.Range(-_xBound, +_xBound);

            // Random powerup generation from the prefabs array
            int powerSelected = Random.Range(0, _powerUpPrefabs.Length);

            // intantiate the powerup
            Instantiate(_powerUpPrefabs[powerSelected], new Vector3(xPos, _yBound, 0), Quaternion.identity, this.transform);
            

        }
    }


    public void StopSpawning()
    {
        if(_spawnEnemies != null)
        {
            StopCoroutine(_spawnEnemies);
        }

        if(_spawnPowerup != null)
        {
            StopCoroutine(_spawnPowerup);
        }  
    }

}
