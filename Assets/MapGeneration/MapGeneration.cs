using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static List<GameObject> mapObjects = new List<GameObject>();
    [SerializeField] AssetGeneration assets;

    public void GenerateFirstFloor()
    {
        foreach (var obj in mapObjects)
        {
            Destroy(obj);
        }
        mapObjects.Clear();
        CafeFloorGenerator.Start();
        CafeWallsGeneration.Start();
        StreetGeneration.Start();
        CameraSetup.FirstFloor();
        assets.Start();
        
    }
    public static void GenerateSecondFloor()
    {
        foreach (var obj in mapObjects)
        {
            Destroy(obj);
        }
        mapObjects.Clear();
        CafeFloorGenerator.Start();
        CafeWallsGeneration.Start();
        StreetGeneration.Start();
        CameraSetup.FirstFloor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
