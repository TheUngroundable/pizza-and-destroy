using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{

    public int value;
    public int valueThreshold = 100;
    public bool collidedWith;
    private bool isObjectHighlyValuable;
    public Player player;

    public void Start(){
        player = GameObject.FindObjectOfType<Player>();
    }

    void Awake(){
        if(value > valueThreshold){
            isObjectHighlyValuable = true;
        }
    }

    void Update()
    {
        
    }
    
    void OnCollisionEnter(Collision collision){
        GameObject collisionGameObject = collision.gameObject;
        if(collisionGameObject.tag == "Player"){
            if(!collidedWith){
                player.wallet -= value;
                player.PlayLosingMoney();
                collidedWith = true;
                if(isObjectHighlyValuable){
                    player.PlayWorried();
                }
            }
        } else if(collisionGameObject.tag == "InteractableObject"){
            InteractableObject interactableObject = collisionGameObject.GetComponent<InteractableObject>();
            if(!interactableObject.collidedWith && !player.playerIsGrabbing){
                interactableObject.collidedWith = true;
                player.wallet -= interactableObject.value;
                player.PlayLosingMoney();
                if(interactableObject.isObjectHighlyValuable){
                    player.PlayWorried();
                }
            }
        }
    }

}
