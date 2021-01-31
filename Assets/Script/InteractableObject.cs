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
    AudioSource audioSource;
    public AudioClip[] collisionSounds;

    public void Start(){
        player = GameObject.FindObjectOfType<Player>();
        audioSource = GetComponent<AudioSource>();
    }

    void Awake(){
        if(value > valueThreshold){
            isObjectHighlyValuable = true;
        }
    }
    
    void OnCollisionEnter(Collision collision){
        GameObject collisionGameObject = collision.gameObject;
        PlayCollisionSounds();
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
            if(!interactableObject.collidedWith && player && !player.playerIsGrabbing){
                interactableObject.collidedWith = true;
                player.wallet -= interactableObject.value;
                player.PlayLosingMoney();
                if(interactableObject.isObjectHighlyValuable){
                    player.PlayWorried();
                }
            }
        }
    }
    void PlayCollisionSounds(){
        int randomIndex = (int) (Random.Range(0f, 10.0f) % collisionSounds.Length);
        AudioClip randomCollisionSounds = collisionSounds[randomIndex];
        if(audioSource){
            audioSource.PlayOneShot(randomCollisionSounds, 0.1f);
        }
    }
}
