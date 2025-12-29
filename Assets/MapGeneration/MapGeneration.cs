using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static List<GameObject> mapObjects = new List<GameObject>();

    public static void Start()
    {
        CafeFloorGenerator.Start();
        CafeWallsGeneration.Start();
        StreetGeneration.Start();
        CameraSetup.Start();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
