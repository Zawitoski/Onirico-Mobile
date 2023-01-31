using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomVisuals : MonoBehaviour
{
    [SerializeField] private Sprite[] floors;
    [SerializeField] private GameObject floorParent;

    public void UpdateVisuals()
    {
        floorParent.GetComponent<SpriteRenderer>().sprite = floors[Random.Range(0, floors.Length)];
    }
}
