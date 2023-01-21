using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    private Vector2 roomSize;

    public bool hasDoor;
    public List<RoomPositionChecker> checkers;
    public List<Vector3> roomAvailablePositions;

    [SerializeField] private List<DungeonController.directions> roomDoorsNeeded;

    private void OnEnable()
    {
        GetComponentInParent<RoomSpawner>().rooms.Add(this);
    }    

    public void UpdatePositions(DungeonController.directions direction, Vector3 position)
    {
        if (direction != DungeonController.directions.none) roomDoorsNeeded.Add(direction);
        if (position != Vector3.zero) roomAvailablePositions.Add(position);
    }

}
