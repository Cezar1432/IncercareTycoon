using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class CarSpwner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    static List<Car> cars= new List<Car>();
    void Start()
    {

    }
    public static float spawnInterval = 2;

    public static float lastSpawn = 0;
    public static float carScale = 0.5f, carSpeed= 5;

    // Update is called once per frame
    public static void Update()
    {
        lastSpawn += Time.deltaTime;
        if (lastSpawn > spawnInterval)
        {
            SpawnCar();
            lastSpawn = 0;//a
        }

        MoveCars();
    }
    public static void MoveCars()
    {

        foreach(Car car in cars)
        {
            Vector3 initialPos = car.car.transform.position;
            float initialZ = initialPos.z;
            if (car.movingForward)
                car.car.transform.position = new Vector3(initialPos.x, initialPos.y, initialZ + carSpeed * Time.deltaTime);
            else
                car.car.transform.position = new Vector3(initialPos.x, initialPos.y, initialZ - carSpeed * Time.deltaTime);

            if(car.car.transform.position.z< 0 || car.car.transform.position.z> 50)
            {
                Destroy(car.car);
                cars.Remove(car);
            }
        }

    }
    public static void SpawnCar()
    {
        Vector3 spawnPose = new Vector3();
        GameObject car = CarList.GetRandomCar();
        int direction = Random.Range(0, 1);
        Quaternion rotation = Quaternion.identity;
        if (direction == 0)
        {
            spawnPose.x = 1;
            spawnPose.z = 0;
        }
        else
        {
            spawnPose.x = 4;
            spawnPose.z = 50;
            rotation = Quaternion.Euler(0, 180, 0);
        }
        car = Instantiate(car, spawnPose, rotation);
        car.transform.localScale = Vector3.one * carScale;







    }
}
