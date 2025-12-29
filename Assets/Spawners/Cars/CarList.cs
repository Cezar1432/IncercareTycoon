using UnityEngine;

public class CarList : MonoBehaviour
{
    // This will show in Inspector
    public GameObject[] carList = new GameObject[25];

    public GameObject GetRandomCar()
    {
        //Debug.Log(carList.Length);
        if (carList.Length == 0) return null;

        int index = Random.Range(0, 25);
        if (carList[index] == null)
            Debug.LogError(index + "is null");

        return carList[index];
    }
}