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
   
   public void DeleteTriggers()
   {
       Destroy(transform.GetChild(6).gameObject);
   }

   public void EnableObjects()
   {
       int rnd = Random.Range(0,roomTypes.Length);
       Debug.Log(rnd);
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
