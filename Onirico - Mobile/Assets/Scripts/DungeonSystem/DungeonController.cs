using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonController : MonoBehaviour
{
    public bool roomsGenerated = false;
    public GameObject currentRoom;
    public List<RoomController> rooms;

    private bool visualsUpdated = false;
    private RoomSpawner roomSpawner => GetComponent<RoomSpawner>();

    private void Update()
    {
        roomsGenerated = roomSpawner.roomsGenerated;

        if (Input.GetKeyDown(KeyCode.P) && !roomsGenerated)
        {
            print("Generate Rooms");
            roomSpawner.GenerateRooms();
        }

        GenerateVisuals();
        UpdateCurrentRoom();
    }

    private void GenerateVisuals()
    {
        if (roomsGenerated && !visualsUpdated)
        {
            foreach (RoomController room in roomSpawner.rooms)
            {
                room.GetComponentInChildren<RoomVisuals>().UpdateVisuals();
            }
            visualsUpdated = true;
        }
    }

    public void UpdateCurrentRoom()
    {
        foreach (var room in rooms)
        {
            if (room.isCurrentRoom) currentRoom = room.gameObject;
        }
    }
}
