using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonWall : MonoBehaviour
{
    public Transform player;
    public GameObject pigeon;
    public int spawnAmount;
    public float spawnOffSetY;
    public float despawnOffsetY;

    private List<GameObject> spawnedObjects;

    private void Start()
    {
        spawnedObjects = new List<GameObject>();
        SpawnWall();
    }

    private void Update()
    {
        if (player == null)
            return;
        if (spawnedObjects[0].transform.position.y > player.position.y + despawnOffsetY)
        {
            GameObject obj = spawnedObjects[0];
            spawnedObjects.RemoveAt(0);
            obj.transform.position = spawnedObjects[spawnedObjects.Count - 1].transform.position + new Vector3(0, -spawnOffSetY);
            spawnedObjects.Add(obj);
        }
        else if (spawnedObjects[spawnedObjects.Count - 1].transform.position.y < player.position.y - despawnOffsetY)
        {
            GameObject obj = spawnedObjects[spawnedObjects.Count - 1];
            spawnedObjects.RemoveAt(spawnedObjects.Count - 1);
            obj.transform.position = spawnedObjects[0].transform.position + new Vector3(0, +spawnOffSetY);
            spawnedObjects.Insert(0, obj);
        }
    }

    void SpawnWall()
    {
        Vector2 spawnPoint = (Vector2)transform.position + new Vector2(0, spawnOffSetY * (spawnAmount * .5f));
        for (int i = 0; i < spawnAmount; i++)
        {
            spawnedObjects.Add(Instantiate(pigeon, spawnPoint, Quaternion.identity, transform));
            spawnPoint += new Vector2(0, -spawnOffSetY);
        }
    }
}
