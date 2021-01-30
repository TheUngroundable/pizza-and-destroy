using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 6f;            // Player's speed when walking.
    public float rotationSpeed = 6f;

    public Rigidbody rb;
    private float targetAngle;
    private Vector3 movement;


    bool playerIsGrabbing = false;
    public float shootingSpeed = 5f;
    public Transform heldObject;
    private Rigidbody heldObjectRB;

    // Using the Awake function to set the references
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Move();
        GrabbingLogics();
        
    }
    void Update()
    {
        RaycastHit hit;   
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
            Debug.DrawRay(transform.position, fwd*1.5f , Color.green);
            if (Physics.Raycast(transform.position, fwd, out hit,1.5f))
            {

                 Debug.Log(hit.transform.name);   
                if(hit.transform.tag=="door")
                { 
                   
                    if(Input.GetKeyDown("space"))
                    {
                        Debug.Log("DOOOOR");   
                        hit.transform.gameObject.SetActive(false);
                    }
                   
                }
                     
                if(hit.transform.tag=="InteractableObject")
                {
                    
                    InteractableObject interactableObject = hit.transform.GetComponent<InteractableObject>();
                    
                    if(playerIsGrabbing){
                        PickUpObject(hit);
                    }
                }
            }
    }
   

    void Move ()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(hAxis, 0f, vAxis).normalized;
        if(direction.magnitude >= 0.1f){
            movement = new Vector3(hAxis, 0f, vAxis);
        }
        rb.position += transform.forward * moveSpeed * Time.deltaTime;
        targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
        rb.rotation = Quaternion.Euler(0f, targetAngle, 0f);
    }

    void PickUpObject(RaycastHit other){
        heldObject = other.transform;
        heldObjectRB = heldObject.GetComponent<Rigidbody>();
        heldObjectRB.isKinematic = true;
        other.transform.SetParent(transform);
    }

    void GrabbingLogics(){
        if(Input.GetKey("space")){
            playerIsGrabbing = true;
        }
        if(Input.GetKeyUp("space")) {
            if(playerIsGrabbing && heldObject != null){
                ThrowHeldObject();
            } 
            playerIsGrabbing = false;
        }
    }
    
    void ThrowHeldObject(){
        heldObject.SetParent(null);
        heldObjectRB.isKinematic = false;
        heldObjectRB.AddForce(transform.forward * shootingSpeed, ForceMode.Impulse);
        heldObject.GetComponent<InteractableObject>().hasBeenThrown = true;
        heldObject = null;
        heldObjectRB = null;
    }
}
