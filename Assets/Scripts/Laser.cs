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
            GameObject.Destroy(this.gameObject);
        }
    }

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
}
