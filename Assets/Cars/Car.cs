using UnityEngine;

public class Car
{
    public GameObject car;
    public bool movingForward;
    public Car(GameObject car, bool movingForward)
    {
        this.car = car;
        this.movingForward = movingForward;
    }
}
