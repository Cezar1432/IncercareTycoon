using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    static Camera mainCamera;
    public static int mapWidth = 50, mapHeight= 50;
    public static void FirstFloor()
    {
        mainCamera = Camera.main;
        if (mainCamera == null )
        {
            GameObject cam = new GameObject("MainCamera");
            mainCamera= cam.AddComponent<Camera>();
            cam.tag = "MainCamera";

        }

        float centerX= mapHeight/2;
        float centerZ= mapWidth/2;

        mainCamera.transform.position = new Vector3(0.8591824f-10, 37.65991f, -0.02502346f);
        mainCamera.transform.LookAt(new Vector3(centerX,0,centerZ));
        mainCamera.orthographic = false;
        mainCamera.orthographicSize = 14;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
