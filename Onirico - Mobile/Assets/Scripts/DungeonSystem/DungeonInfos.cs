using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonInfos : MonoBehaviour
{
    [SerializeField] private int maxRooms = 25;
    [SerializeField] private int minRooms = 5;
    [SerializeField] private int roomsToSpawn;
    [SerializeField] private Vector2 roomSize = new Vector2(9, 16);
    [Range(0, 1)]
    [Tooltip("Chance de spawn por ponto disponível")]
    [SerializeField] private float roomSpawnChance = 1;
    [Range(0, 1)]
    [SerializeField] private float roomSpawnInterval = 0f;


    public static int MaxRooms;
    public static int MinRooms;
    public static int RoomsToSpawn;
    public static float RoomSpawnChance;
    public static float RoomSpawnInterval;
    public static Vector2 RoomSizes;
    public enum directions { Left, Right, Top, Bottom, none };

    private void Awake()
    {
        MaxRooms = maxRooms;
        MinRooms = minRooms;
        RoomSizes = roomSize;
        RoomSpawnChance = roomSpawnChance;
        RoomSpawnInterval = roomSpawnInterval;
        roomsToSpawn = RoomsToSpawn = Random.Range(MaxRooms, MinRooms);
    }
}
