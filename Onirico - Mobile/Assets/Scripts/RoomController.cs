using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    private Vector2 roomSize;

    public bool hasDoor;
    public List<RoomPositionChecker> checkers;
    public List<RoomPositionChecker> roomAvailablePositions;

    [SerializeField] private List<DungeonController.directions> roomDoorsNeeded;

    private void OnEnable()
    {
        GetComponentInParent<RoomSpawner>().rooms.Add(this);
    }

    public void UpdatePositions(DungeonController.directions direction, RoomPositionChecker checker)
    {
        if (direction != DungeonController.directions.none) roomDoorsNeeded.Add(direction);
        if (checker != null && !roomAvailablePositions.Contains(checker)) roomAvailablePositions.Add(checker);
    }
}
