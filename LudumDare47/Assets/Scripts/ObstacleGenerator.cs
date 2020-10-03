using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject[] obstacles;
    public int spawnAmount;
    public float spawnOffsetX;
    public float spawnOffsetY;
    public float spawnVarianceX;
    public float spawnVarianceY;
    private Transform player;
    private List<GameObject> spawnedObstacles;

    private void Start()
    {
        spawnedObstacles = new List<GameObject>();
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if (spawnedObstacles.Count < spawnAmount) //Spawn Obstacles
        {
            Vector2 spawnPosition = player.position + new Vector3(spawnOffsetX, 0);
            for (int i = 0; i < spawnAmount; i++)
            {
                if (i == 0)
                    spawnedObstacles.Add(Instantiate(obstacles[Random.Range(0, obstacles.Length - 1)], spawnPosition, Quaternion.identity, transform));
                else
                {
                    spawnedObstacles.Add(Instantiate(obstacles[Random.Range(0, obstacles.Length - 1)], spawnPosition + new Vector2(Random.Range(-spawnVarianceX, spawnVarianceX), spawnOffsetY * i + Random.Range(-spawnVarianceY, spawnVarianceY)), Quaternion.identity, transform));
                    spawnedObstacles.Add(Instantiate(obstacles[Random.Range(0, obstacles.Length - 1)], spawnPosition + new Vector2(Random.Range(-spawnVarianceX, spawnVarianceX), -spawnOffsetY * i + Random.Range(-spawnVarianceY, spawnVarianceY)), Quaternion.identity, transform));
                }
            }
        }
        else
        {
            for (int i = 0; i < spawnedObstacles.Count; i++)
            {
                //transform.Translate(Vector2.left * 0.01f * Time.DeltaTime);

                if (spawnedObstacles[i].transform.position.x < player.position.x - spawnOffsetX * 0.5f) //Remove Obstacles
                {
                    GameObject go = spawnedObstacles[i];
                    spawnedObstacles.RemoveAt(i);
                    Destroy(go);
                }
            }
        }
    }
}
