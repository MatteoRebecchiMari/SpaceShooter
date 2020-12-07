using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Player reference
    Player _player;

    // Start is called before the first frame update
    void Start()
    {
        // Player reference in the scene
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovements();
    }

    // Speed velocity unit/second.
    [SerializeField]
    float _speed = 4f;

    [SerializeField]
    float _yBound = 6.7f;

    [SerializeField]
    float _xBound = 11.3f;

    // Calculate movements of the player
    void CalculateMovements()
    {

        // Move down the enemy
        transform.Translate(Vector3.down * _speed * Time.deltaTime, Space.World);

        // If the object arrive to the end on the bounds, we move the position in the opposite limit
        int flipY = Mathf.Abs(transform.position.y) > _yBound ? -1 : 1;

        float yPos = Mathf.Clamp(transform.position.y * flipY, -_yBound, _yBound);

        float xPos = transform.position.x;

        // When we move the object on TOP, we set a random X potion
        if (flipY == -1)
        {
            xPos = Random.Range(-_xBound, +_xBound);
        }

        // Apply the new position
        transform.position = new Vector3(xPos, yPos, 0);
    }

    // Unity Events called when two objects 3D collides: NOT USED NOW
    void OnTriggerEnter(Collider other)
    {
        // When we hit the player
        Player playerHitten = other.gameObject.GetComponent<Player>();
        if (playerHitten)
        {
            Debug.Log("Enemy: i hit the player");

            // Damage the player
            playerHitten.Damage();

            // Destroy myself
            Destroy(this.gameObject);
        }
    }

    // Unity Events called when two objects 2D collides
    void OnTriggerEnter2D(Collider2D other)
    {
        // When we hit the player
        Player playerHitten = other.gameObject.GetComponent<Player>();
        if (playerHitten)
        {
            Debug.Log("Enemy: i hit the player");

            // Damage the player
            playerHitten.Damage();

            // Destroy myself
            Destroy(this.gameObject);
        }

        // When we hit an enemy
        Laser hittenLaser = other.gameObject.GetComponent<Laser>();
        if (hittenLaser)
        {
            Debug.Log("Enemy: i was hitten by a laser");

            // Improve player score
            if (_player)
            {
                _player.AddScore(10);
            }

            // Destroy the laser
            Destroy(hittenLaser.gameObject);

            // Now i can destroy myself
            Destroy(this.gameObject);
        }

    }

}
