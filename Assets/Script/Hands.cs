using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{


    bool playerIsGrabbing = false;
    public float shootingSpeed = 5f;
    public GameObject heldObject;
    private Rigidbody heldObjectRB;
    
    void Update()
    {
         if(Input.GetKeyDown("space")){
            playerIsGrabbing = true;
        }
        if(Input.GetKeyUp("space")) {
            if(playerIsGrabbing && heldObject != null){
                throwHeldObject();
            } else {
                playerIsGrabbing = false;
            }
        }
    }

    void throwHeldObject(){
        playerIsGrabbing = false;
        Debug.Log("Throw Item");
        heldObject.transform.SetParent(null);
        heldObjectRB.isKinematic = false;
        heldObjectRB.AddForce(transform.forward * shootingSpeed, ForceMode.Impulse);
        heldObject.GetComponent<InteractableObject>().hasBeenThrown = true;
        heldObject = null;
        heldObjectRB = null;
    }
    void pickUpObject(Collider other){
        heldObject = other.gameObject;
        heldObjectRB = heldObject.GetComponent<Rigidbody>();
        heldObjectRB.isKinematic = true;
        other.transform.SetParent(transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        InteractableObject interactableObject = other.GetComponent<InteractableObject>();
        if (other.gameObject.tag == "InteractableObject" && playerIsGrabbing && !interactableObject.hasBeenThrown)
        {
           pickUpObject(other);
        }
    }
}
