using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    //Single Items
    public GameObject roomPrefab;
    [SerializeField] private int currentRooms;
    [SerializeField] private Transform parentTransform;

    //Lists
    public List<RoomController> rooms;
    [SerializeField] private List<RoomPositionChecker> AvailableCheckers;
    [SerializeField] private List<Vector3> ignoredPositions = new List<Vector3>();

    private void Start()
    {
        UpdateAvailableSpawnPositions();
    }
    private void Update()
    {
        currentRooms = rooms.Count;

        if (Input.GetKeyDown(KeyCode.P)) GenerateDungeonSequence();
    }


    //-------------------------------------------------------------------------------------------------------

    private void GenerateDungeonSequence()
    {
        var currentAvailablePositions = UpdateAvailableSpawnPositions();
        var selectedPositions = SelectSpawnPositions(currentAvailablePositions);
        var filteredSpawnPositions = FilterSpawnPositions(selectedPositions);
        SpawnRooms(filteredSpawnPositions);
    }

    private List<Vector3> UpdateAvailableSpawnPositions()
    {
        AvailableCheckers.Clear();
        List<Vector3> availableSpawnPositions = new List<Vector3>();

        //Adiciona todas as novas posições possíveis baseado nos novos checkers
        foreach (RoomController room in rooms)
        {
            foreach (RoomPositionChecker checker in room.roomAvailablePositions)
                if (!checker.hasRoom) AvailableCheckers.Add(checker);
                else if (AvailableCheckers.Contains(checker)) AvailableCheckers.Remove(checker);
        }

        //Transforma posição dos Checkers em uma lista Vector3
        foreach (RoomPositionChecker checker in AvailableCheckers)
            availableSpawnPositions.Add(checker.transform.position);

        //Filtra as posições ignoradas existentes dentro da lista de posições válidas
        foreach (Vector3 position in availableSpawnPositions.ToArray())
            foreach (Vector3 ignoredPosition in ignoredPositions) if (position == ignoredPosition) availableSpawnPositions.Remove(position);

        return availableSpawnPositions;
    }

    private List<Vector3> SelectSpawnPositions(List<Vector3> availablePositions)
    {
        if (AvailableCheckers.Count <= 0) return null;

        List<Vector3> selectedPositions = new List<Vector3>();
        var placeholderRandom = 0.5f;

        foreach (Vector3 position in availablePositions)
        {
            placeholderRandom = Random.Range(0f, 1f);
            if (placeholderRandom > DungeonController.RoomSpawnChance && !selectedPositions.Contains(position))
                selectedPositions.Add(position);
            else ignoredPositions.Add(position);
        }

        return selectedPositions;
    }

    private List<Vector3> FilterSpawnPositions(List<Vector3> selectedSpawnPositions)
    {
        var maxRooms = DungeonController.RoomsToSpawn;
        if (rooms.Count == maxRooms)
        {
            print("All allowed Rooms Spawned");
            return null;
        }

        var previewedSpawnedRooms = rooms.Count + selectedSpawnPositions.Count;
        var oversizeAmount = previewedSpawnedRooms - maxRooms;
        print("Previewed Spawned Rooms = " + previewedSpawnedRooms);
        print("Oversize Amount = " + oversizeAmount);

        if (oversizeAmount > 0)
            foreach (Vector3 position in selectedSpawnPositions.ToArray())
            {
                selectedSpawnPositions.Remove(position);
                oversizeAmount--;
            }

        return selectedSpawnPositions;
    }

    private void SpawnRooms(List<Vector3> spawnPositions)
    {
        if (spawnPositions == null)
        {
            print("No available Spawn Positions");
            return;
        }

        foreach (Vector3 position in spawnPositions)
        {
            Instantiate(roomPrefab, position, Quaternion.identity, parentTransform);
        }
    }
}
