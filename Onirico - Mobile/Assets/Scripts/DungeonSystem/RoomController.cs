using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public List<RoomPositionChecker> checkers;
    public List<RoomPositionChecker> roomAvailablePositions;

    [SerializeField] private List<DungeonInfos.directions> roomDoorsNeeded;

    private void Start() => GetComponentInParent<RoomSpawner>().rooms.Add(this);

    public void UpdatePositions(DungeonInfos.directions direction, RoomPositionChecker checker, bool hasRoom)
    {
        if (hasRoom) roomDoorsNeeded.Add(direction);
        if (!hasRoom && !roomAvailablePositions.Contains(checker)) roomAvailablePositions.Add(checker);
    }
}
