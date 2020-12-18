using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Destory the explosion after 3 secs
        Destroy(gameObject, 2.65f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
