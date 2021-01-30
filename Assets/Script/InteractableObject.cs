using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{

    public int value;
    public bool collidedWith;
    public Player player;
    void Start(){
        
    }

    void Update()
    {
        
    }
    
    void OnCollisionEnter(Collision collision){
        GameObject collisionGameObject = collision.gameObject;
        if(collisionGameObject.tag == "Player"){
            if(!collidedWith){
                player.wallet -= value;
                collidedWith = true;
            }
        } else if(collisionGameObject.tag == "InteractableObject"){
            InteractableObject interactableObject = collisionGameObject.GetComponent<InteractableObject>();
            if(!interactableObject.collidedWith && !player.playerIsGrabbing){
                interactableObject.collidedWith = true;
                player.wallet -= interactableObject.value;
            }
        }
    }

}
