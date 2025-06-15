using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject carPreFab;
    public Transform[] spawnPoints; // 顺序应为 up, down, right, left
    public TrafficLightController intersectionCtrl;

    private float timer;
    private float environemntTimer;
    private float incEveryTenSec = 10f;
    public float timeInterval = 2f;
    public float minInterval = 0.3f;

    void Update()
    {
        timer += Time.deltaTime;
        environemntTimer += Time.deltaTime;

        if (environemntTimer >= incEveryTenSec)
        {
            timeInterval = Mathf.Max(minInterval, timeInterval - 0.2f);
            environemntTimer = 0f;
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

        GameObject car = Instantiate(carPreFab, spawnPoint.position, spawnPoint.rotation);
        Car carScript = car.GetComponent<Car>();

        carScript.myDirection = (CarDirection)index;
        carScript.intersectionCtrl = intersectionCtrl;
    }
}