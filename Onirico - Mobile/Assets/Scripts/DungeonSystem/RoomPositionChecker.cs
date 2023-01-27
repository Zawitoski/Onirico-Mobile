using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPositionChecker : MonoBehaviour
{
    private RoomController roomController;
    private Vector2 roomSize;

    public bool hasRoom = false;
    public DungeonInfos.directions myDirection;

    private void Awake() => roomController = GetComponentInParent<RoomController>();

    private void Start()
    {
        roomSize = DungeonInfos.RoomSizes;
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
            roomController.UpdatePositions(myDirection, this, true);
            return;
        }

        switch (myDirection)
        {
            case DungeonInfos.directions.Left:
                gameObject.transform.position = transform.position + new Vector3(roomSize.x * -1, 0f);
                break;
            case DungeonInfos.directions.Right:
                gameObject.transform.position = transform.position + new Vector3(roomSize.x, 0f);
                break;
            case DungeonInfos.directions.Top:
                transform.position = transform.position + new Vector3(0f, roomSize.y);
                break;
            case DungeonInfos.directions.Bottom:
                gameObject.transform.position = transform.position + new Vector3(0f, roomSize.y * -1);
                break;
        }
        roomController.UpdatePositions(DungeonInfos.directions.none, this, false);
    }
}
