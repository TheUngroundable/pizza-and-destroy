using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sca : MonoBehaviour
{
 public   float moveSpeed = 6f;            // Player's speed when walking.
 public float rotationSpeed = 6f;
 public float jumpHeight = 10f;         // How high Player jumps

public Vector3 moveDirection;

public Rigidbody rb;

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

    Vector3 movement = new Vector3(hAxis, 0f, vAxis);
    rb.position += movement * moveSpeed * Time.deltaTime;
    
     
            float x = Input.GetAxis("Horizontal") * rotationSpeed;
             float y = Input.GetAxis("Vertical") * rotationSpeed;
             Vector3  mov = new Vector3(x, 0f, y);
              mov = transform.TransformDirection(mov);
             rb.AddForce(mov);
                       
}
}
