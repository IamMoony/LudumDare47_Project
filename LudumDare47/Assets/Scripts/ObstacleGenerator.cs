using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject[] obstacles;
    public int spawnAmount;
    public float initialSpawnOffsetX;
    public float initialSpawnOffsetY;
    public float spawnVarianceX;
    public float spawnVarianceY;
    public float despawnOffsetX;
    public float despawnOffsetY;
    private Transform player;
    private List<GameObject> spawnedObstacles;

    private void Start()
    {
        spawnedObstacles = new List<GameObject>();
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if (player == null)
            return;
        if (spawnedObstacles.Count < spawnAmount) //Spawn Obstacles
        {
            Vector2 spawnPosition = player.position + new Vector3(initialSpawnOffsetX, 0);
            for (int i = 0; i < spawnAmount - spawnedObstacles.Count; i++)
            {
                Vector2 spawnOffset = new Vector2(0, Random.Range(-spawnVarianceY, spawnVarianceY));
                if (spawnOffset.y > initialSpawnOffsetY || spawnOffset.y < -initialSpawnOffsetY)
                    spawnOffset.x = Random.Range(-(spawnVarianceX + initialSpawnOffsetX), spawnVarianceX);
                else
                    spawnOffset.x = Random.Range(0, spawnVarianceX);
                spawnedObstacles.Add(Instantiate(obstacles[Random.Range(0, obstacles.Length)], spawnPosition + spawnOffset, Quaternion.identity, transform));
            }
        }
        else
        {
            for (int i = 0; i < spawnedObstacles.Count; i++)
            {
                if (spawnedObstacles[i] == null)
                {
                    spawnedObstacles.RemoveAt(i);
                    return;
                }
                if (spawnedObstacles[i].transform.position.x < player.position.x - despawnOffsetX || spawnedObstacles[i].transform.position.y < player.position.y - despawnOffsetY || spawnedObstacles[i].transform.position.y > player.position.y + despawnOffsetY) //Remove Obstacles
                {
                    GameObject go = spawnedObstacles[i];
                    spawnedObstacles.RemoveAt(i);
                    Destroy(go);
                }
            }
        }
    }
}
