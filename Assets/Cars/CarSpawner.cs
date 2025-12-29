using Unity.Collections;
using UnityEngine;

public class CarSpwner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    public static float spawnInterval = 2;

    public static float lastSpawn = 0;
    public static float carScale = 0.5f;

    // Update is called once per frame
    public static void Update()
    {
        lastSpawn += Time.deltaTime;
        if (lastSpawn > spawnInterval)
        {
            SpawnCar();
        }

        MoveCars();
    }
    public static void MoveCars()
    {



    }
    public static void SpawnCar()
    {
        Vector3 spawnPose = new Vector3();
        GameObject car = CarList.GetRandomCar();
        int direction = Random.Range(0, 1);
        Quaternion rotation = Quaternion.identity;
        if (direction == 0)
        {
            spawnPose.x = 4;
            spawnPose.z = 0;
        }
        else
        {
            spawnPose.x = 1;
            spawnPose.z = 50;
            rotation = Quaternion.Euler(0, 180, 0);
        }
        car = Instantiate(car, spawnPose, rotation);
        car.transform.localScale = Vector3.one * carScale;







    }
}
