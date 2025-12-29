using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class CarSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     List<Car> cars= new List<Car>();
    void Start()
    {

    }
    public  float spawnInterval = 2;

    public  float lastSpawn = 0;
    public  float carScale = 0.5f, carSpeed= 5;

    // Update is called once per frame
    public void UpdateCarBehaviour()
    {
        lastSpawn += Time.deltaTime;
        if (lastSpawn > spawnInterval)
        {
            SpawnCar();
            lastSpawn = 0;//a
        }

        MoveCars();
    }
    public void MoveCars()
    {

        for(int i= cars.Count -1 ; i>= 0; i--) 
        {
            Car car= cars[i];
            Vector3 initialPos = car.car.transform.position;
            float initialZ = initialPos.z;
            if (car.movingForward)
                car.car.transform.position = new Vector3(initialPos.x, initialPos.y, initialZ + carSpeed * Time.deltaTime);
            else
                car.car.transform.position = new Vector3(initialPos.x, initialPos.y, initialZ - carSpeed * Time.deltaTime);

            if(car.car.transform.position.z< 0 || car.car.transform.position.z> 50)
            {
                Destroy(car.car);
                cars.RemoveAt(i);
            }
        }

    }

    [SerializeField]private GameObject[] carList= new GameObject[25];
    public  GameObject getRandomCar()
    {
        int index = Random.Range(0, 25);
        return carList[index];
    }
     CarList listaMasini = new CarList();
    public  void SpawnCar()
    {
        Vector3 spawnPose = new Vector3();
        GameObject car = getRandomCar();
        if (car != null)
        {
            int direction = Random.Range(0, 2);
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
            car.transform.localScale = Vector3.one * carScale * 2;
            cars.Add(new Car(car, direction == 0));
        }
        else
        {
            Debug.LogError("car is null");
        }







    }
}
