using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSystem : MonoBehaviour
{
    public GameObject currentRoom;

    [SerializeField] private CinemachineConfiner cameraBounds;

    private void Update()
    {
        currentRoom = FindObjectOfType<DungeonController>().currentRoom;

        cameraBounds.m_BoundingShape2D = currentRoom.GetComponentInChildren<PolygonCollider2D>();
    }
}
