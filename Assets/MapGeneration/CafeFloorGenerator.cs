using UnityEngine;

public class CafeFloorGenerator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static void Start()
    {
       for(int i= 4; i< 46; i++)
            for(int j= 7; j< 47; j++)
            {
                GameObject floorTile = getFloorTile(new Vector3(j, 0, i));
                MapGeneration.mapObjects.Add(floorTile);
                
            }
    }

    static GameObject getFloorTile(Vector3 pos)
    {
        GameObject floorTile = new GameObject("FloorTile");
        floorTile.transform.position = pos;
        MeshFilter filter= floorTile.AddComponent<MeshFilter>();
        MeshRenderer renderer = floorTile.AddComponent<MeshRenderer>();
        Mesh mesh= new Mesh();
        Vector3[] verticies = new Vector3[4]
        {
            new Vector3(0, 0, 0)
            ,
            new Vector3(1, 0, 0)
            ,
            new Vector3(1, 0, 1)
            ,
            new Vector3(0, 0, 1)
        };


        int[] triangles = new int[6]{0, 2, 1, 0, 3, 2};
        mesh.vertices = verticies;

        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        filter.mesh = mesh;
        Material mat = new Material(Shader.Find("Unlit/Color"));
        mat.color = Color.sandyBrown;
        renderer.material = mat;

        return floorTile;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
