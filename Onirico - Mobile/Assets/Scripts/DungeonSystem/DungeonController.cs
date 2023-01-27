using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonController : MonoBehaviour
{
    public bool roomsGenerated = false;

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

        if (roomsGenerated && !visualsUpdated)
        {
            foreach (RoomController room in roomSpawner.rooms)
            {
                room.GetComponentInChildren<RoomVisuals>().UpdateVisuals();
            }
            visualsUpdated = true;
        }
    }


}
