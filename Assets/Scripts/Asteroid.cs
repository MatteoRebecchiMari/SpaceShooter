using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _speedRotation = 3.0f;

    private float _direction = 1;

    // Prefab for the explosion
    [SerializeField]
    private GameObject _explosionPrefab;

    private Collider2D _collider2D;

    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        _direction = Random.Range(0, 2) == 0 ? 1 : - 1;
        _collider2D = GetComponent<Collider2D>();
        if(_collider2D == null)
        {
            Debug.LogError("Asteriod: Collider 2D not found");
        }
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the object trow the z axis
        gameObject.transform.Rotate(Vector3.forward, _speedRotation * Time.deltaTime * _direction);
    }


    // Collision triggered, for 3D Object: NOT USED NOW
    void OnTriggerEnter2D(Collider2D other)
    {
        // When we hit an enemy
        Laser hittenLaser = other.gameObject.GetComponent<Laser>();
        if (hittenLaser)
        {
            // Disable the collider
            _collider2D.enabled = false;

            // Instantiate the explosion in the asteroid position
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            // Stop to rotate
            _speedRotation = 0;

            // Destoy the laser
            Destroy(hittenLaser.gameObject);

            // START SPAWNING ENEMIES
            _spawnManager.StartSpawning();

            // Now i can destroy myself
            Destroy(this.gameObject, 0.2f);

            
        }
    }
}
