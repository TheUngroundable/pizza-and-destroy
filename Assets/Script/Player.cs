using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float moveSpeed = 6f;            // Player's speed when walking.
    public float rotationSpeed = 6f;

    private Rigidbody rb;
    private float targetAngle;
    private Vector3 movement;


    public bool playerIsGrabbing = false;
    public float shootingSpeed = 5f;
    public Transform heldObject;
    private Rigidbody heldObjectRB;

    public int wallet = 0;
    public Text walletText;

    private AudioSource audioSource;
    public AudioClip[] randomTaunts;

    public AudioClip[] randomPickUp;
    public AudioClip[] randomThrow;
    public AudioClip[] foundMoney;
    public AudioClip[] worried;

    public AudioClip earningMoney;
    public AudioClip losingMoney;
    public AudioClip[] steps;

    public float tauntsProbability = 1f;

    // Using the Awake function to set the references
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("PlayStepsSound", 0.1f, 0.3f);
    }

    void FixedUpdate()
    {
        Move();
        GrabbingLogics();
    }
    void Update()
    {
        ManageWalletUI();
        ManageRaycast();
        PlayTaunts();
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
        /* if(Input.GetKey("space") && heldObject == null){
            playerIsGrabbing = true;
        }
        if(Input.GetKeyUp("space") && heldObject != null){
            ThrowHeldObject();
            PlayThrowSound();
    
            playerIsGrabbing = false;
        } */

        if(Input.GetKeyDown("space")){
            if(heldObject == null){
                playerIsGrabbing = true;
            } else {
                playerIsGrabbing = false;
                ThrowHeldObject();
                PlayThrowSound();
            }
        }
    }
    
    void ThrowHeldObject(){
        heldObject.SetParent(null);
        heldObjectRB.isKinematic = false;
        heldObjectRB.AddForce(transform.forward * shootingSpeed, ForceMode.Impulse);
        heldObject = null;
        heldObjectRB = null;
    }

    void ManageRaycast(){
        RaycastHit hit;   
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, out hit,1.5f))
        {
            if(hit.transform.tag=="door")
            { 
                if(Input.GetKeyDown("space"))
                {
                    hit.transform.GetComponent<Door>().PlayOpenSound();
                    hit.transform.gameObject.SetActive(false);
                }
            }  
            if(hit.transform.tag=="InteractableObject")
            {
                InteractableObject interactableObject = hit.transform.GetComponent<InteractableObject>();
                if(playerIsGrabbing && heldObject == null){
                    PickUpObject(hit);
                    PlayPickUpSound();
                    playerIsGrabbing = false;
                }
            }
        }
    }
    void ManageWalletUI(){
        walletText.text = "Wallet "+wallet;
    }
    void PlayTaunts(){
        float playProbability = Random.Range(0f, 100.0f);
        if(!audioSource.isPlaying && playProbability < (tauntsProbability / 5)){
            int randomIndex = (int) (Random.Range(0f, 10.0f) % randomTaunts.Length);
            AudioClip randomTaunt = randomTaunts[randomIndex];
            PlaySound(randomTaunt);
        }
    }

    void PlayPickUpSound(){
        int randomIndex = (int) (Random.Range(0f, 10.0f) % randomPickUp.Length);
        AudioClip randomPickUpSound = randomPickUp[randomIndex];
        PlaySound(randomPickUpSound);
    }

    void PlayThrowSound(){
        int randomIndex = (int) (Random.Range(0f, 10.0f) % randomThrow.Length);
        AudioClip randomThrowSound = randomThrow[randomIndex];
        PlaySound(randomThrowSound);
    }

    void PlayFoundMoney(){
        int randomIndex = (int) (Random.Range(0f, 10.0f) % foundMoney.Length);
        AudioClip foundMoneySound = foundMoney[randomIndex];
        PlaySound(foundMoneySound);
    }

    public void PlayWorried(){
        int randomIndex = (int) (Random.Range(0f, 10.0f) % worried.Length);
        AudioClip worriedSound = worried[randomIndex];
        PlaySound(worriedSound);
    }
    public void PlayEarningMoney(){
        PlaySound(earningMoney);
    }

    public void PlayLosingMoney(){
        PlaySound(losingMoney);
    }

    public void PlayStepsSound(){
        int randomIndex = (int) (Random.Range(0f, 10.0f) % steps.Length);
        AudioClip randomStepsSound = steps[randomIndex];
        PlaySound(randomStepsSound);
    }

    public void PlaySound(AudioClip audioClip){
        audioSource.PlayOneShot(audioClip, 0.7F);
    }
    
    void OnCollisionEnter(Collision collision){
        GameObject collisionGameObject = collision.gameObject;
        if(collisionGameObject.tag == "Money") {
            Money money = collisionGameObject.GetComponent<Money>();
            wallet += money.value;
            Destroy(collisionGameObject);
            PlayEarningMoney();
            PlayFoundMoney();
        }
    }
}
