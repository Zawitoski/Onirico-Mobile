using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPositionChecker : MonoBehaviour
{
    [SerializeField]private DungeonController.directions myDirection;
    private RoomController roomController;
    private bool hasRoom = false;
    private Vector2 roomSize;

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
        if (!hasRoom)
        {
            var sideDistance = roomSize.x;
            var topDistance = roomSize.y;
            var placeholderPosition = transform.position;
            switch (myDirection)
            {
                case DungeonController.directions.Left:
                    placeholderPosition = new Vector3(transform.position.x + sideDistance * -1f, transform.position.y, 0f);
                    this.gameObject.transform.position = transform.position + new Vector3(roomSize.x * -1, 0f);
                    break;
                case DungeonController.directions.Right:
                    placeholderPosition = new Vector3(transform.position.x + sideDistance, transform.position.y, 0f);
                    this.gameObject.transform.position = transform.position + new Vector3(roomSize.x, 0f);
                    break;
                case DungeonController.directions.Top:
                    placeholderPosition = new Vector3(transform.position.x, transform.position.y + topDistance, 0f);
                    this.transform.position = transform.position + new Vector3(0f, roomSize.y);
                    break;
                case DungeonController.directions.Bottom:
                    placeholderPosition = new Vector3(transform.position.x, transform.position.y + topDistance * -1f, 0f);
                    this.gameObject.transform.position = transform.position + new Vector3(0f, roomSize.y * -1);
                    break;
            }
            roomController.UpdatePositions(DungeonController.directions.none, placeholderPosition);
            return;
        }
        roomController.UpdatePositions(myDirection, Vector3.zero);
    }
}
