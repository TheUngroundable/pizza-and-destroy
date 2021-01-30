using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 6f;            // Player's speed when walking.
    public float rotationSpeed = 6f;

    public Rigidbody rb;
    private float targetAngle;
    public Camera cam;
    // Using the Awake function to set the references
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move ()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(hAxis, 0f, vAxis).normalized;
        if(direction.magnitude >= 0.1f) {
            Vector3 movement = new Vector3(hAxis, 0f, vAxis);
            rb.position += movement * moveSpeed * Time.deltaTime;
            targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        }
        
    }
}
