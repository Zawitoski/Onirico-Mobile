using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [HideInInspector] public bool roomsGenerated = false;

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
    private void Update() => currentRooms = rooms.Count;

    //-------------------------------------------------------------------------------------------------------

    public void GenerateRooms()
    {
        print("Generating Rooms...");
        StartCoroutine(GenerateDungeonSequence());
    }

    public IEnumerator GenerateDungeonSequence()
    {
        if (roomsGenerated) yield break;

        yield return new WaitForSeconds(DungeonInfos.RoomSpawnInterval);

        var currentAvailablePositions = UpdateAvailableSpawnPositions();
        var selectedPositions = SelectSpawnPositions(currentAvailablePositions);
        var filteredSpawnPositions = FilterSpawnPositions(selectedPositions);
        SpawnRooms(filteredSpawnPositions);
        StartCoroutine(GenerateDungeonSequence());
    }

    private List<Vector3> UpdateAvailableSpawnPositions()
    {
        AvailableCheckers.Clear();
        List<Vector3> availableSpawnPositions = new List<Vector3>();

        //Adiciona todas as novas posições possíveis baseado nos novos checkers
        foreach (RoomController room in rooms)
            foreach (RoomPositionChecker checker in room.roomAvailablePositions)
            {
                if (!checker.hasRoom)
                {
                    AvailableCheckers.Add(checker);
                    availableSpawnPositions.Add(checker.transform.position);
                }
                else if (AvailableCheckers.Contains(checker)) AvailableCheckers.Remove(checker);
            }

        //Filtra e remove as posições ignoradas existentes dentro da lista de posições válidas
        foreach (Vector3 position in availableSpawnPositions.ToArray())
            foreach (Vector3 ignoredPosition in ignoredPositions)
                if (position == ignoredPosition) availableSpawnPositions.Remove(position);

        return availableSpawnPositions;
    }

    private List<Vector3> SelectSpawnPositions(List<Vector3> availablePositions)
    {
        if (AvailableCheckers.Count <= 0) return null;

        List<Vector3> selectedPositions = new List<Vector3>();

        foreach (Vector3 position in availablePositions)
        {
            if (Random.Range(0f, 1f) > DungeonInfos.RoomSpawnChance && !selectedPositions.Contains(position))
                selectedPositions.Add(position);
            else ignoredPositions.Add(position);
        }

        return selectedPositions;
    }

    private List<Vector3> FilterSpawnPositions(List<Vector3> selectedSpawnPositions)
    {
        var maxRooms = DungeonInfos.RoomsToSpawn;
        if (rooms.Count == maxRooms)
        {
            print("All Rooms Spawned: " + rooms.Count + " rooms.");
            roomsGenerated = true;
            return null;
        }

        var previewedSpawnedRooms = rooms.Count + selectedSpawnPositions.Count;
        var oversizeAmount = previewedSpawnedRooms - maxRooms;
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
        if (spawnPositions == null) return;

        foreach (Vector3 position in spawnPositions)
        {
            Instantiate(roomPrefab, position, Quaternion.identity, parentTransform);
        }
    }
}
