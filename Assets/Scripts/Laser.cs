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
}
