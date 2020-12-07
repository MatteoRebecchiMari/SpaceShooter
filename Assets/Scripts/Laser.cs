using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    float _speed = 8;

    [SerializeField]
    float _maxHeight = 6f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move the laser up
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        // Auto-destruction
        if(transform.position.y > _maxHeight)
        {
            DestroyParent();
            GameObject.Destroy(this.gameObject);
        }
    }

    void DestroyParent()
    {
        if(this.transform.parent != null)
        {
            GameObject.Destroy(this.transform.parent.gameObject);
        }
    }

    // Collision triggered, for 3D Object: NOT USED NOW
    void OnTriggerEnter(Collider other)
    {
        // When we hit an enemy
        Enemy hittenEnemy = other.gameObject.GetComponent<Enemy>();
        if (hittenEnemy)
        {
            Debug.Log("Laser: i hit the Enemy");
            Destroy(hittenEnemy.gameObject);

            // Now i can destroy myself
            Destroy(this.gameObject);
        }
    }

    // Collision triggered, for 2D object
    void OnTriggerEnter2D(Collider2D other)
    {
        // When we hit an enemy
        //Enemy hittenEnemy = other.gameObject.GetComponent<Enemy>();
        //if (hittenEnemy)
        //{
        //    Debug.Log("Laser: i hit the Enemy");
        //
        //    // Now i can destroy myself
        //    Destroy(this.gameObject);
        //}
    }
}
