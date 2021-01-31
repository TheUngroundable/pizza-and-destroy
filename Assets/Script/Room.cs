using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public float maxMoney;
    public Transform doorA;
    public Transform doorB;
    public Transform doorC;
    public Transform doorD;
    public GameObject[] roomTypes;
    public GameObject doors;
    public Color[] colorA; 
    public Color[] colorB; 
    public Material matA;
    public Material matB;



    void Start()
    {
        int rnd = Random.Range(0,colorA.Length);
        matA.color = colorA[rnd];
        matB.color = colorB[rnd];

       
        transform.GetChild(1).GetComponent<Renderer>().materials[0].color = colorA[rnd];
        transform.GetChild(1).GetComponent<Renderer>().materials[1].color = colorB[rnd];
        transform.GetChild(2).GetComponent<Renderer>().materials[0].color = colorA[rnd];
        transform.GetChild(2).GetComponent<Renderer>().materials[1].color = colorB[rnd];
        transform.GetChild(3).GetComponent<Renderer>().materials[0].color = colorA[rnd];
        transform.GetChild(3).GetComponent<Renderer>().materials[1].color = colorB[rnd];
        transform.GetChild(4).GetComponent<Renderer>().materials[0].color = colorA[rnd];
        transform.GetChild(4).GetComponent<Renderer>().materials[1].color = colorB[rnd];
        
    }

   public void DeleteTriggers()
   {
       Destroy(transform.GetChild(6).gameObject);
   }

   public void EnableObjects()
   {
       int rnd = Random.Range(0,roomTypes.Length);
       roomTypes[rnd].SetActive(true);
   }

   public void ShowDoors()
   {
      StartCoroutine(WaitForOpen());
   }

   IEnumerator WaitForOpen()
   {
       yield return new WaitForSeconds(.3f);
        doors.SetActive(true);
   }
}
