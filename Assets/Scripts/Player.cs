using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Speed velocity unit/second.
    [SerializeField]
    float _speed = 5.0f;

    [SerializeField]
    float _yBound = 9.0f;

    [SerializeField]
    float _xBound = 11.0f;

    // Start is called before the first frame update
    void Start()
    {
        // set the object in the zero position
        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
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
}
