using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("Object creation")]

    public GameObject prefabToSpawn;

    // The key to press to create the objects/projectiles
    public KeyCode keyToPress = KeyCode.Space;

    [Header("Other options")]

    // The rate of creation, as long as the key is pressed
    public float creationRate = .5f;

    // The speed at which the object are shot along the Y axis
    public float shootSpeed = 5f;

    public Vector2 shootDirection = new Vector2(1f, 1f);

    public bool relativeToRotation = true;

    private float timeOfLastSpawn;

    // Will be set to 0 or 1 depending on how the GameObject is tagged


    // Use this for initialization
    void Start()
    {
        timeOfLastSpawn = -creationRate;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(keyToPress) && Time.time >= timeOfLastSpawn + creationRate)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Vector2 actualBulletDirection = (relativeToRotation) ? (Vector2)(Quaternion.Euler(0, 0, transform.eulerAngles.z) * shootDirection) : shootDirection;

        GameObject newObject = Instantiate<GameObject>(prefabToSpawn);
        newObject.transform.position = this.transform.position;
        newObject.transform.eulerAngles = new Vector3(0f, 0f, Angle(actualBulletDirection));
        newObject.tag = "Bullet";

        // push the created objects, but only if they have a Rigidbody2D
        Rigidbody2D rigidbody2D = newObject.GetComponent<Rigidbody2D>();
        if (rigidbody2D != null)
        {
            rigidbody2D.AddForce(actualBulletDirection * shootSpeed, ForceMode2D.Impulse);
        }

        BulletAttribute b = newObject.GetComponent<BulletAttribute>();
        if (b == null)
        {
            b = newObject.AddComponent<BulletAttribute>();
        }

        timeOfLastSpawn = Time.time;
    }

    private float Angle(Vector2 inputVector)
    {
        if (inputVector.x < 0) return (Mathf.Atan2(inputVector.x, inputVector.y) * Mathf.Rad2Deg * -1) - 360;
        else return -Mathf.Atan2(inputVector.x, inputVector.y) * Mathf.Rad2Deg;
    }
}
