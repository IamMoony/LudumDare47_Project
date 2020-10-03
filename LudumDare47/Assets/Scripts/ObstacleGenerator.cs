using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject[] obstacles;
    public int spawnAmount;
    public float initialSpawnOffsetX;
    public float spawnOffsetX;
    public float spawnOffsetY;
    public float spawnVarianceX;
    public float spawnVarianceY;
    private Transform player;
    private List<GameObject> spawnedObstacles;
    private int waveCount;

    private void Start()
    {
        spawnedObstacles = new List<GameObject>();
        player = GameObject.Find("Player").transform;
        waveCount = 0;
    }

    private void Update()
    {
        if (spawnedObstacles.Count < spawnAmount) //Spawn Obstacles
        {
            Vector2 spawnPosition = player.position + new Vector3(waveCount == 0 ? initialSpawnOffsetX : spawnOffsetX, 0);
            waveCount++;
            for (int i = 0; i < spawnAmount; i++)
            {
                if (i == 0)
                    spawnedObstacles.Add(Instantiate(obstacles[Random.Range(0, obstacles.Length)], spawnPosition, Quaternion.identity, transform));
                else
                {
                    spawnedObstacles.Add(Instantiate(obstacles[Random.Range(0, obstacles.Length)], spawnPosition + new Vector2(Random.Range(-spawnVarianceX, spawnVarianceX), spawnOffsetY * i + Random.Range(-spawnVarianceY, spawnVarianceY)), Quaternion.identity, transform));
                    spawnedObstacles.Add(Instantiate(obstacles[Random.Range(0, obstacles.Length)], spawnPosition + new Vector2(Random.Range(-spawnVarianceX, spawnVarianceX), -spawnOffsetY * i + Random.Range(-spawnVarianceY, spawnVarianceY)), Quaternion.identity, transform));
                }
            }
        }
        else
        {
            for (int i = 0; i < spawnedObstacles.Count; i++)
            {
                //transform.Translate(Vector2.left * 0.01f * Time.DeltaTime);

                if (spawnedObstacles[i].transform.position.x < player.position.x - spawnOffsetX) //Remove Obstacles
                {
                    GameObject go = spawnedObstacles[i];
                    spawnedObstacles.RemoveAt(i);
                    Destroy(go);
                }
            }
        }
    }
}
