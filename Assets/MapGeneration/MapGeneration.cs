using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static void Start()
    {
        StreetGeneration.Start();
        CameraSetup.Start();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
