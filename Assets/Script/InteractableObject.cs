using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{

    public bool hasBeenThrown = false;
    bool containsMoney = false;
    public float objectDropProbability;
    public GameObject moneyPrefab;
    void Start(){
        if(Random.Range(-10.0f, 10.0f) % objectDropProbability == 0){
            containsMoney = true;
        }
    }

    void Update()
    {
        
    }
 
    private void OnCollisionEnter(Collision collision)
    {
        if(hasBeenThrown && containsMoney){
            Instantiate(moneyPrefab, transform.position, Quaternion.identity);
        }
    }
 
}
