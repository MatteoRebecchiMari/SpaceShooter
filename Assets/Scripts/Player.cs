using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // SpawnManager reference
    SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        // Get the sp manager from the scene
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovements();
        HandleLaserShooting();
    }

    // Speed velocity unit/second.
    [SerializeField]
    float _speed = 10f;

    [SerializeField]
    float _yBound = 6.7f;

    [SerializeField]
    float _xBound = 11.3f;

    // Calculate movements of the player
    void CalculateMovements()
    {
        // Get input using unity input manager (See Windows menu --> InputManager --> Axis)
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        // Get the motion from the input and apply to the object
        Vector3 direction = new Vector3(horizontalAxis, verticalAxis, 0);
        transform.Translate(direction * Time.deltaTime * _speed, Space.World);

        // If the object arrive to the end on the bounds, we move the position in the opposite limit
        int flipX = Mathf.Abs(transform.position.x) > _xBound ? -1 : 1;
        int flipY = Mathf.Abs(transform.position.y) > _yBound ? -1 : 1;

        // LImit the motion using the bounds
        float xPos = Mathf.Clamp(transform.position.x * flipX, -_xBound, _xBound);
        float yPos = Mathf.Clamp(transform.position.y * flipY, -_yBound, _yBound);

        // Apply the new position
        transform.position = new Vector3(xPos, yPos, 0);
    }

    // Laser prefab reference
    [SerializeField]
    GameObject _laserPrefab;

    [SerializeField]
    Vector3 _laserSpawnOffset = Vector3.zero;

    [SerializeField]
    float _fireRate = 0.5f;

    // The next time you can shot
    float _nextFire = 0f;

    // Handle laser shooting
    void HandleLaserShooting()
    {
        // Hit the space key, we spown the laser
        if(Input.GetKey(KeyCode.Space) && Time.time > _nextFire)
        {
            // Calculate the next available time to shot, relative to the fire rate
            _nextFire = Time.time + _fireRate;

            // Spawn the laser in the current positon of the player
            GameObject laser = GameObject.Instantiate(_laserPrefab, transform.position + _laserSpawnOffset, Quaternion.identity);

        }
    }

    // Life of the player
    int _lifes = 3;

    // Apply damage to the player
    public void Damage()
    {
        _lifes --;

        // Game over
        if(_lifes <= 0)
        {
            // Stop generating enemies
            _spawnManager.StopSpawning();

            Destroy(this.gameObject);
        }
    }

    
    void OnTriggerEnter(Collider other)
    {

    }

}
