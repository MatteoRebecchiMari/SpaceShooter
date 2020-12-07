using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enums to indentify powerups
public enum PowerupType
{
    TripleShot,
    Speed,
    Shields,
}

public class PowerUp : MonoBehaviour
{
    // The type of powerup
    [SerializeField]
    private PowerupType _type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovements();
    }

    [SerializeField]
    float _speed = 3f;

    [SerializeField]
    float _yBound = 6.7f;

    //[SerializeField]
    //float _xBound = 11.3f;

    void HandleMovements()
    {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        // We are at the end of the worls
        if(transform.position.y < -_yBound)
        {
            Destroy(this.gameObject);
        }

    }

    // Manage Collision with the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player hittenPlayer = collision.gameObject.GetComponent<Player>();
        if (hittenPlayer)
        {
      
            switch (_type)
            {
                case PowerupType.TripleShot:
                    // Enable the triple shot for the player
                    hittenPlayer.TripleShotActive();
                    break;
                case PowerupType.Speed:
                    // Enable superspeed for the player
                    hittenPlayer.IncreaseSpeed();
                    break;
                case PowerupType.Shields:
                    // Enable shield for the player
                    hittenPlayer.ShieldActive();
                    break;
            }

            // Destory myself
            Destroy(this.gameObject);
        }
    }

}
