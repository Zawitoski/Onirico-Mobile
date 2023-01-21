using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonController : MonoBehaviour
{
    [SerializeField] private int maxRooms = 25;
    [SerializeField] private int minRooms = 5;
    [SerializeField] private Vector2 roomSize = new Vector2(9,16);


    public static int MaxRooms;
    public static int MinRooms;
    public static Vector2 RoomSizes;
    public enum directions { Left, Right, Top, Bottom, none };

    private void Awake()
    {
        MaxRooms = maxRooms;
        MinRooms = minRooms;
        RoomSizes = roomSize;
    }
}
