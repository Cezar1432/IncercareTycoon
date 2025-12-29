using UnityEngine;

public class CarList : MonoBehaviour
{
    // This will show in Inspector
    public static GameObject[] carList = new GameObject[25];

    public static GameObject GetRandomCar()
    {
        if (carList.Length == 0) return null;
        return carList[Random.Range(0, carList.Length)];
    }
}