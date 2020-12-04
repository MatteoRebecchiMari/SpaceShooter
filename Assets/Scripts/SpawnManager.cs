using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    // Prefab for the enemy
    [SerializeField]
    GameObject _enemyPrefab;

    [SerializeField]
    float _yBound = 6.7f;

    [SerializeField]
    float _xBound = 11.3f;

    Coroutine _spawnEnemies;

    // Start is called before the first frame update
    void Start()
    {
        _spawnEnemies = StartCoroutine(SpawnEnemiesRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        StopCoroutine(_spawnEnemies);
    }

    public void StopSpawning()
    {
        StopCoroutine(_spawnEnemies);
    }

    // Coroutine to spawn enemies
    IEnumerator SpawnEnemiesRoutine()
    {
        // Skip 1 frame
        yield return null;

        while (true)
        {
            // suspend execution for X seconds
            float waitTimeSec = Random.Range(1f, 3f);
            yield return new WaitForSeconds(waitTimeSec);

            // Spawn the enemy
            // Random x Position
            float xPos = Random.Range(-_xBound, +_xBound);

            // Generate the enemy in the random position
            Instantiate(_enemyPrefab, new Vector3(xPos, _yBound, 0), Quaternion.identity, this.transform);

        }
    }


}
