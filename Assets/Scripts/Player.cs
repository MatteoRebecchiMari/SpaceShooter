using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    // SpawnManager reference
    SpawnManager _spawnManager;

    UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        // Get the sp manager from the scene
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("Player: SpawnManager NOT FOUND!");
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("Player: UIManager NOT FOUND!");
        }

        // Load the audio source
        _audioSource = GetComponent<AudioSource>();
        if(_audioSource == null)
        {
            Debug.LogError("Player: AudioSource NOT FOUND!");
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovements();
        HandleLaserShooting();
    }

    // Speed velocity unit/second.
    [SerializeField]
    float _speed = 5f;

    [SerializeField]
    float _yBound = 6.7f;

    [SerializeField]
    float _xBound = 11.3f;

    // Calculate movements of the player
    void CalculateMovements()
    {

        // Get input using unity input manager (See Windows menu --> InputManager --> Axis)
        // CROSS PLATFROM INPUT MANAGER
        float horizontalAxis = CrossPlatformInputManager.GetAxis("Horizontal"); //Input.GetAxis("Horizontal");
        float verticalAxis = CrossPlatformInputManager.GetAxis("Vertical"); //Input.GetAxis("Vertical");


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


    [SerializeField]
    GameObject _tripleShotPrefab;

    // Is Triple Shot Active
    [SerializeField]
    bool _isTripleShotActive;

    // Laser audio source
    [SerializeField]
    AudioClip _laserAudioClip;

    [SerializeField]
    AudioClip _powerUpAudioClip;

    // Audio source that pplay a sound
    AudioSource _audioSource;

    // Handle laser shooting
    void HandleLaserShooting()
    {
        // Hit the space key, we spown the laser
        // For CrossPlatformInputManager.GetButtonDown("Fire") --> See ButtonHandler component Name=Fire

        if ((Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("Fire")) && Time.time > _nextFire)
        {
            // Calculate the next available time to shot, relative to the fire rate
            _nextFire = Time.time + _fireRate;

            if (_isTripleShotActive)
            {
                // Fire 3 lasers
                GameObject tripleShot = Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
                
            }
            else
            {
                // Normal shot

                // Spawn the laser in the current positon of the player
                GameObject laser = Instantiate(_laserPrefab, transform.position + _laserSpawnOffset, Quaternion.identity);
            }


            // Play LASER Sound
            // Set the audio laser clip
            if (_laserAudioClip == null)
            {
                Debug.LogError("No laser clip to play");
            }
            else
            {
                _audioSource.PlayOneShot(_laserAudioClip);
            }

        }
    }

    void PlayPowerUpSound()
    {
        // Play PowerUp Sound
        // Set the audio laser clip
        if (_powerUpAudioClip == null)
        {
            Debug.LogError("No powerup clip to play");
        }
        else
        {
            _audioSource.PlayOneShot(_powerUpAudioClip);
        }
    }

    // Life of the player
    [SerializeField]
    int _lives = 3;

    [SerializeField]
    GameObject _shiled2DVisualizer;

    // Shield activation
    [SerializeField]
    bool _isShieldActive = false;

    [SerializeField]
    GameObject _rightEngine, _leftEngine;

    // Apply damage to the player
    public void Damage()
    {
        // Shield protect the life until the player is hitten
        if (_isShieldActive)
        {
            _isShieldActive = false;
            _shiled2DVisualizer.SetActive(false);
            return;
        }

        _lives --;

        switch (_lives)
        {
            case 2:
                _rightEngine.SetActive(true);
                break;
            case 1:
                _leftEngine.SetActive(true);
                break;
        }

        // Update lives in the UI
        if (_uiManager)
        {
            _uiManager.UpdateLives(_lives);
        }

        // Game over
        if (_lives <= 0)
        {
            // Stop generating enemies
            _spawnManager.StopSpawning();

            Destroy(this.gameObject);
        }
    }

    // Enabled the triple shot
    public void TripleShotActive()
    {       
        StartCoroutine(TripleShotCoroutine(5));
        PlayPowerUpSound();
    }

    // Enabled super speed
    public void IncreaseSpeed()
    {
        StartCoroutine(SpeedIncreaseCoroutine(5));
        PlayPowerUpSound();
    }

    // Enabled shiled
    public void ShieldActive()
    {
        // Enable the shield
        _isShieldActive = true;
        _shiled2DVisualizer.SetActive(true);
        //StartCoroutine(ShieldCoroutine(5));
        PlayPowerUpSound();
    }

    // Coroutine to handle triple shot activation
    IEnumerator TripleShotCoroutine(float activeTime)
    {

        // disable triple shot
        _isTripleShotActive = true;

        // Skip 1 frame
        yield return null;

        yield return new WaitForSeconds(activeTime);

        // disable triple shot
        _isTripleShotActive = false;

    }

    // Coroutine to handle super speed activation
    IEnumerator SpeedIncreaseCoroutine(float activeTime)
    {
        
        // Increase the speed
        _speed = 10.0f;

        yield return null;

        yield return new WaitForSeconds(activeTime);

        // Return to normal speed
        _speed = 5.0f;
      
    }

    // Corotune for shield
    IEnumerator ShieldCoroutine(float activeTime)
    {
        // Enable the shield
        _isShieldActive = true;

        // Skip 1 frame
        yield return null;

        yield return new WaitForSeconds(activeTime);

        // Disable the shield
        _isShieldActive = false;
    }

    // Score of the player
    [SerializeField]
    int _score = 0;

    // Add score to the player
    public void AddScore(int score)
    {
        _score += score;

        // Update the UI score using the ui manager
        if (_uiManager)
        {
            _uiManager.UpdateScore(_score);
        }
    }

}
