using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [SerializeField] private int currentRooms;
    [Range(0,1)]
    [Tooltip("Chance de spawn por ponto disponível")]
    [SerializeField] private float roomSpawnChance = 1;
    [SerializeField] private List<Vector3> availableSpawnPositions;
    [SerializeField] private Transform parentTransform;

    private List<Vector3> lastAvailablePositions = new List<Vector3>();
    public GameObject roomPrefab;
    public List<RoomController> rooms;

    private void Start()
    {
        UpdateAvailableSpawnPositions(DungeonController.RoomSizes);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) SpawnRooms(SelectSpawnPositions());
    }

    private List<Vector3> SelectSpawnPositions()
    {
        print("Select Spawn Positions");
        foreach(Vector3 position in availableSpawnPositions)
        {
            if(lastAvailablePositions != null)
                foreach(Vector3 lastPosition in lastAvailablePositions)
                {
                    if (lastPosition != position)
                        availableSpawnPositions.Add(position);
                }
        }
        lastAvailablePositions.Clear();
        return lastAvailablePositions = availableSpawnPositions;
    }

    private void SpawnRooms(List<Vector3> spawnPositions)
    {
        print("Spawn Rooms");
        foreach(Vector3 position in spawnPositions)
        {
            Instantiate(roomPrefab, position, Quaternion.identity, parentTransform);
        }
    }

    private void UpdateAvailableSpawnPositions(Vector2 roomSize)
    {
        foreach (RoomController room in rooms)
        {
            foreach (Vector3 position in room.roomAvailablePositions)
                availableSpawnPositions.Add(position);
        }
    }
}
