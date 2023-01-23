using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPositionChecker : MonoBehaviour
{
    private RoomController roomController;
    private Vector2 roomSize;

    public bool hasRoom = false;
    public string Name => GetComponentInParent<RoomController>().GetComponent<Transform>().name + " - " + GetComponent<Transform>().name;
    public DungeonController.directions myDirection;

    private void Awake()
    {
        roomController = GetComponentInParent<RoomController>();
    }

    private void Start()
    {
        roomSize = DungeonController.RoomSizes;
        UpdateRoomCheckers();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Room")) hasRoom = true;
    }

    public void UpdateRoomCheckers()
    {
        if (hasRoom)
        {
            roomController.UpdatePositions(myDirection, null);
            return;
        }

        switch (myDirection)
        {
            case DungeonController.directions.Left:
                gameObject.transform.position = transform.position + new Vector3(roomSize.x * -1, 0f);
                break;
            case DungeonController.directions.Right:
                gameObject.transform.position = transform.position + new Vector3(roomSize.x, 0f);
                break;
            case DungeonController.directions.Top:
                transform.position = transform.position + new Vector3(0f, roomSize.y);
                break;
            case DungeonController.directions.Bottom:
                gameObject.transform.position = transform.position + new Vector3(0f, roomSize.y * -1);
                break;
        }
        roomController.UpdatePositions(DungeonController.directions.none, this);
    }
}
