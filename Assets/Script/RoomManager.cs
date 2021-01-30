using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject roomPrefab;
    public List<Room> rooms = new List<Room>();
    
    void Start()
    {
        InstanceFirstRoom();
    }


    

    public void InstanceFirstRoom()
    {
        GameObject curRoom = Instantiate(roomPrefab,transform);
        curRoom.transform.localPosition = new Vector3(0,0,0);
        Room rm = curRoom.GetComponent<Room>();
        rm.EnableObjects();
        rm.DeleteTriggers();
        rm.ShowDoors();   
        rooms.Add(rm);

        curRoom = null;
        curRoom = Instantiate(roomPrefab,transform);
        curRoom.transform.localPosition = rooms[0].doorA.localPosition;
        rooms.Add(curRoom.GetComponent<Room>());

        curRoom = null;
        curRoom = Instantiate(roomPrefab,transform);
        curRoom.transform.localPosition = rooms[0].doorB.localPosition;
        rooms.Add(curRoom.GetComponent<Room>());

        curRoom = null;
        curRoom = Instantiate(roomPrefab,transform);
        curRoom.transform.localPosition = rooms[0].doorC.localPosition;
        rooms.Add(curRoom.GetComponent<Room>());

        curRoom = null;
        curRoom = Instantiate(roomPrefab,transform);
        curRoom.transform.localPosition = rooms[0].doorD.localPosition;
        rooms.Add(curRoom.GetComponent<Room>());
/*
        curRoom = null;
        curRoom = Instantiate(roomPrefab,transform);
        curRoom.transform.position = rooms[1].doorD.position;
        rooms.Add(curRoom.GetComponent<Room>());

        curRoom = null;
        curRoom = Instantiate(roomPrefab,transform);
        curRoom.transform.position = rooms[3].doorD.position;
        rooms.Add(curRoom.GetComponent<Room>());

        curRoom = null;
        curRoom = Instantiate(roomPrefab,transform);
        curRoom.transform.position = rooms[1].doorB.position;
        rooms.Add(curRoom.GetComponent<Room>());

        curRoom = null;
        curRoom = Instantiate(roomPrefab,transform);
        curRoom.transform.position = rooms[3].doorB.position;
        rooms.Add(curRoom.GetComponent<Room>());
        */
    }


    public void EnteredRoom(Room room, string direction )
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if(rooms[i] != room)
            {
               Destroy(rooms[i].gameObject);   
            }
        }  
         rooms.RemoveAll(x => x != room);
      
        if(direction!="triggerA")
        {
            GameObject curRoom = Instantiate(roomPrefab,transform);
            curRoom.transform.position = room.doorA.position;
            rooms.Add(curRoom.GetComponent<Room>());
        }

         if(direction!="triggerB")
        {
            GameObject curRoom = Instantiate(roomPrefab,transform);
            curRoom.transform.position = room.doorB.position;
            rooms.Add(curRoom.GetComponent<Room>());
        }

         if(direction!="triggerC")
        {
            GameObject curRoom = Instantiate(roomPrefab,transform);
            curRoom.transform.position = room.doorC.position;
            rooms.Add(curRoom.GetComponent<Room>());
        }

         if(direction!="triggerD")
        {
            GameObject curRoom = Instantiate(roomPrefab,transform);
            curRoom.transform.position = room.doorD.position;
            rooms.Add(curRoom.GetComponent<Room>());
        }
        
    }
   
}
