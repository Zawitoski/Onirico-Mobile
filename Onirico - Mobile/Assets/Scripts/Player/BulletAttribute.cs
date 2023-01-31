using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttribute : MonoBehaviour
{
    public GameObject efeito;

    private void Reset()
    {
        Instantiate(efeito, transform.position, Quaternion.identity);
    }
}
