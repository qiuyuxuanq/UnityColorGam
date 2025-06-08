using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject carPreFab;
    private float timer;
    private float environemntTimer;
    private float incEveryTenSec = 10f;
    public float timeInterval = 2f;
    public float minInterval = 0.3f; // 设定最小生成间隔，防止太快
    public Transform[] spawnPoints;

    void Update()
    {
        timer += Time.deltaTime;
        environemntTimer += Time.deltaTime;

        if (environemntTimer >= incEveryTenSec)
        {
            timeInterval = Mathf.Max(minInterval, timeInterval - 0.2f);
            environemntTimer = 0f; // 别忘了重置环境计时器
        }

        if (timer >= timeInterval)
        {
            SpawnCar();
            timer = 0f;
        }
    }

    void SpawnCar()
    {
        int index = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[index];

        Instantiate(carPreFab, spawnPoint.position, spawnPoint.rotation);
    }
}
