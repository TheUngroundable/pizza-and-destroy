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
    public bool talkedToPizzaBoy = false;

    public MoneyText textMoney;
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
        ManageRaycast();
        PlayTaunts();
    }

    void Move()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(hAxis, 0f, vAxis).normalized;
        if (direction.magnitude >= 0.1f)
        {
            movement = new Vector3(hAxis, 0f, vAxis);
        }
        rb.position += transform.forward * moveSpeed * Time.deltaTime;
        targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
        rb.rotation = Quaternion.Euler(0f, targetAngle, 0f);
    }

    void PickUpObject(RaycastHit other)
    {
        heldObject = other.transform;
        heldObjectRB = heldObject.GetComponent<Rigidbody>();
        heldObjectRB.isKinematic = true;
        other.transform.SetParent(transform);
    }

    void GrabbingLogics()
    {
        if (Input.GetKeyDown("space"))
        {
            if (heldObject == null)
            {
                playerIsGrabbing = true;
            }
            else
            {
                playerIsGrabbing = false;
                ThrowHeldObject();
                PlayThrowSound();
            }
        }
    }

    void ThrowHeldObject()
    {
        heldObject.SetParent(null);
        heldObjectRB.isKinematic = false;
        heldObjectRB.AddForce(transform.forward * shootingSpeed, ForceMode.Impulse);
        heldObject = null;
        heldObjectRB = null;
    }

    void ManageRaycast()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, out hit, 1.5f))
        {
            if (hit.transform.tag == "door")
            {
                if (Input.GetKeyDown("space"))
                {
                    hit.transform.gameObject.GetComponent<Door>().PlayOpenSound();
                    hit.transform.gameObject.SetActive(false);
                }
            }
            if (hit.transform.tag == "fattorino")
            {
                if (Input.GetKeyDown("space") && !talkedToPizzaBoy)
                {
                    hit.transform.GetComponent<Pizzaboy>().isPizzaboyWaiting = false;
                    talkedToPizzaBoy = true;
                    GameObject.FindObjectOfType<RoomManager>().StartGame();
                }
            }

            if (hit.transform.tag == "InteractableObject")
            {
                InteractableObject interactableObject = hit.transform.GetComponent<InteractableObject>();
                if (playerIsGrabbing && heldObject == null)
                {
                    PickUpObject(hit);
                    PlayPickUpSound();
                    playerIsGrabbing = false;
                }
            }
        }
    }

    void PlayTaunts()
    {
        float playProbability = Random.Range(0f, 100.0f);
        if (!audioSource.isPlaying && playProbability < (tauntsProbability / 5))
        {
            PlaySound(AudioHelper.GetRandomAudioClip(randomTaunts));
        }
    }

    void PlayPickUpSound()
    {
        PlaySound(AudioHelper.GetRandomAudioClip(randomPickUp));
    }

    void PlayThrowSound()
    {
        PlaySound(AudioHelper.GetRandomAudioClip(randomThrow));
    }

    void PlayFoundMoney()
    {
        PlaySound(AudioHelper.GetRandomAudioClip(foundMoney));
    }

    public void PlayWorried()
    {
        PlaySound(AudioHelper.GetRandomAudioClip(worried));
    }
    public void PlayEarningMoney()
    {
        PlaySound(earningMoney);
    }

    public void PlayLosingMoney()
    {
        PlaySound(losingMoney);
    }

    public void PlayStepsSound()
    {
        PlaySound(AudioHelper.GetRandomAudioClip(steps));
    }

    public void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip, 0.7F);
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.tag == "Money")
        {
            Money money = collisionGameObject.GetComponent<Money>();
            wallet += money.value;
            Destroy(collisionGameObject);
            PlayEarningMoney();
            PlayFoundMoney();
            SpawnMoneyText(money.value);
        }
    }


     public void SpawnMoneyText(int monez)
    {
        GameObject curText = Instantiate(textMoney.gameObject);
        curText.transform.position = transform.position + new Vector3(0,2,0);
        curText.GetComponent<MoneyText>().money = monez;
    }
}
