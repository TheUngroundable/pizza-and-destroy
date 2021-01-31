using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{

    public float topValue;
    public float lowValue;
    public int value;
    public int valueThreshold = 100;
    public bool collidedWith;
    private bool isObjectHighlyValuable;
    public Player player;
    AudioSource audioSource;
    public AudioClip[] collisionSounds;
    public MoneyText textMoney;

    
    public void Start(){
        player = GameObject.FindObjectOfType<Player>();
        audioSource = GetComponent<AudioSource>();
        value = GetRandomValue();
    }

    void Awake(){
        if(value > valueThreshold){
            isObjectHighlyValuable = true;
        }
    }
    
    int GetRandomValue(){
        return (int) Random.Range(lowValue, topValue);
    }

    void OnCollisionEnter(Collision collision){
        GameObject collisionGameObject = collision.gameObject;
        PlayCollisionSounds();
        if(collisionGameObject.tag == "Player"){
            if(!collidedWith){
                player.wallet -= value;
                player.PlayLosingMoney();
                collidedWith = true;
                SpawnMoneyText();
                if(isObjectHighlyValuable){
                    player.PlayWorried();
                }
            }
        } else if(collisionGameObject.tag == "InteractableObject"){
            InteractableObject interactableObject = collisionGameObject.GetComponent<InteractableObject>();
            if(interactableObject && !interactableObject.collidedWith && player && !player.playerIsGrabbing){
                interactableObject.collidedWith = true;
                player.wallet -= interactableObject.value;
                player.PlayLosingMoney();
                SpawnMoneyText();
                if(interactableObject.isObjectHighlyValuable){
                    player.PlayWorried();
                }
            }
        }
    }

     
    public void SpawnMoneyText()
    {
        GameObject curText = Instantiate(textMoney.gameObject);
        curText.transform.position = transform.position + new Vector3(0,2,0);
        if(value == 0){
            value = GetRandomValue();
        }
        curText.GetComponent<MoneyText>().money = -value;
    }

    void PlayCollisionSounds(){
        int randomIndex = (int) (Random.Range(0f, 10.0f) % collisionSounds.Length);
        AudioClip randomCollisionSounds = collisionSounds[randomIndex];
        if(audioSource){
            audioSource.PlayOneShot(randomCollisionSounds, 0.1f);
        }
    }
}
