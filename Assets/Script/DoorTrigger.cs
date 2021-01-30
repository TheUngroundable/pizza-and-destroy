using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Room room;

    void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag=="Player")
        {
            room.EnableObjects();
            room.DeleteTriggers(); //distruggo trigger quando entro nella stanza cosi da non richiamare la funzione quando esco
            room.ShowDoors();
            RoomManager rm = GameObject.FindObjectOfType<RoomManager>();
            rm.EnteredRoom(room,transform.name);
            Destroy(this.gameObject);
        }
    }
}
