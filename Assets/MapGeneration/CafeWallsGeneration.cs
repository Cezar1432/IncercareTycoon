using UnityEngine;

public class CafeWallsGeneration : MonoBehaviour


{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static void Start()
    {
        //front wall
        for(int j=0; j< 5; j++)
        for(int i = 4; i< 46; i++)
        {
            GameObject wall = getWall(new Vector3(7, j, i), ((i== 25 || i==24) && (j==0 || j==1 || j==2)));
            MapGeneration.mapObjects.Add(wall);
        }

    }

    static GameObject getWall(Vector3 pos, bool isDoor)
    {
        GameObject wall = new GameObject("wall cube");
        wall.transform.position = pos;
        MeshFilter filter = wall.AddComponent<MeshFilter>();
        MeshRenderer renderer= wall.AddComponent<MeshRenderer>();
        Mesh mesh= new Mesh();

        Vector3[] vertices =
        {
            new Vector3(0,0,0),
            new Vector3(1,0,0),
            new Vector3(1,0,1),
            new Vector3(0,0,1),

            new Vector3(0,1,0),
            new Vector3(1,1,0),
            new Vector3(1,1,1),
            new Vector3(0,1,1)
        };

        int[] trisSingle =
 {
    // bottom
    0,2,1, 0,3,2,
    // top
    4,5,6, 4,6,7,
    // front
    0,1,5, 0,5,4,
    // back
    3,7,6, 3,6,2,
    // left
    0,4,7, 0,7,3,
    // right
    1,2,6, 1,6,5
};

        // make double-sided: add reversed triangles
        int[] trisDouble = new int[trisSingle.Length * 2];
        for (int i = 0; i < trisSingle.Length; i += 3)
        {
            // original
            trisDouble[i] = trisSingle[i];
            trisDouble[i + 1] = trisSingle[i + 1];
            trisDouble[i + 2] = trisSingle[i + 2];

            // reversed (for inward-facing)
            int j = trisSingle.Length + i;
            trisDouble[j] = trisSingle[i];
            trisDouble[j + 1] = trisSingle[i + 2];
            trisDouble[j + 2] = trisSingle[i + 1];
        }

        mesh.vertices = vertices;
        mesh.triangles = trisDouble;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        filter.mesh = mesh;
        var mat = new Material(Shader.Find("Unlit/Color"));
        if(isDoor)
            mat.color = Color.rosyBrown;
        else
            mat.color = Color.saddleBrown;
        renderer.material = mat;
        return wall;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
