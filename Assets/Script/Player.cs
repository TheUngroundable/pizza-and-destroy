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

            RaycastHit hit;   
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            Debug.DrawRay(transform.position, fwd*1.5f , Color.green);
            if (Physics.Raycast(transform.position, fwd, out hit,1.5f))
            {
                Debug.Log("cacaca");
                Debug.Log(hit.transform.name);
                if(hit.transform.tag=="door")
                     Debug.Log("ketched");
            }
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
