using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    public GameObject demoRoom;
    public GameObject roomPrefab;
    public List<Room> rooms = new List<Room>();
    public float countDown = 5f;
    public float timeRemaining = 10;
    public Text timerUI;
    public int winMoney = 500;
    public Player pl;
    private bool gameStarted = false;
    private AudioSource audioSource;
    public AudioClip softMusic;
    public AudioClip metalMusic;

    void Start()
    {
        Application.targetFrameRate = 60;
        InstanceFirstRoom();
        audioSource = GetComponent<AudioSource>();
        PlaySoftMusic();
    }

    void PlaySoftMusic()
    {
        audioSource.PlayOneShot(softMusic);
    }
    void PlayMetalMusic()
    {
        audioSource.loop = true;
        audioSource.clip = metalMusic;
        audioSource.Stop();
        audioSource.Play();
    }
    public void InstanceFirstRoom()
    {
        GameObject curRoom = Instantiate(demoRoom, transform);
        curRoom.transform.localPosition = new Vector3(0, 0, 0);
        Room rm = curRoom.GetComponent<Room>();
        rm.DeleteTriggers();
        rm.ShowDoors();
        rooms.Add(rm);

        curRoom = null;
        curRoom = Instantiate(roomPrefab, transform);
        curRoom.transform.localPosition = rooms[0].doorA.localPosition;
        rooms.Add(curRoom.GetComponent<Room>());

        curRoom = null;
        curRoom = Instantiate(roomPrefab, transform);
        curRoom.transform.localPosition = rooms[0].doorB.localPosition;
        rooms.Add(curRoom.GetComponent<Room>());

        curRoom = null;
        curRoom = Instantiate(roomPrefab, transform);
        curRoom.transform.localPosition = rooms[0].doorC.localPosition;
        rooms.Add(curRoom.GetComponent<Room>());


    }

    public void Update()
    {
        if (gameStarted)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                timerUI.text = timeRemaining.ToString("f0");
            }
            else
            {
                Debug.Log("fine parza");
                EndGame();
            }
        }
    }

    public void StartGame()
    {
        rooms[0].transform.GetChild(10).gameObject.SetActive(false);
        PlayMetalMusic();
        StartCoroutine(StartGameAfterSeconds());
    }

    public void EndGame()
    {
        if (pl.wallet >= winMoney)
        {
            //vinci
            Debug.Log("VINTOOO");
        }
        else
        {
            //hai perso
            Debug.Log("PERSOOO");
        }
    }
    IEnumerator StartGameAfterSeconds()
    {
        rooms[0].transform.GetChild(11).gameObject.SetActive(true);
        yield return new WaitForSeconds(countDown);
        //inizia la partita , le porte si abilitano ,iniza musica e gli oggetti in scena di abilitano
        rooms[0].transform.GetChild(11).gameObject.SetActive(false);
        rooms[0].transform.GetChild(8).gameObject.SetActive(false);
        gameStarted = true;

    }


    public void EnteredRoom(Room room, string direction)
    {
        for (int i = 0; i < rooms.Count - 4; i++)
        {
            if (rooms[i] != room)
            {
                Destroy(rooms[i].gameObject);
            }
        }
        rooms.RemoveAll(x => x != room);

        if (direction != "triggerA")
        {
            GameObject curRoom = Instantiate(roomPrefab, transform);
            curRoom.transform.position = room.doorA.position;
            rooms.Add(curRoom.GetComponent<Room>());
        }

        if (direction != "triggerB")
        {
            GameObject curRoom = Instantiate(roomPrefab, transform);
            curRoom.transform.position = room.doorB.position;
            rooms.Add(curRoom.GetComponent<Room>());
        }

        if (direction != "triggerC")
        {
            GameObject curRoom = Instantiate(roomPrefab, transform);
            curRoom.transform.position = room.doorC.position;
            rooms.Add(curRoom.GetComponent<Room>());
        }

        if (direction != "triggerD")
        {
            GameObject curRoom = Instantiate(roomPrefab, transform);
            curRoom.transform.position = room.doorD.position;
            rooms.Add(curRoom.GetComponent<Room>());
        }

    }

}
