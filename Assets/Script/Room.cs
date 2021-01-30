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
   
   public void DeleteTriggers()
   {
       Destroy(transform.GetChild(6).gameObject);
   }
}
