using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StreetGeneration : MonoBehaviour
{

    [Header("Map Settings")]
    static int  mapWidth = 50;
    static int mapHeight = 50;
    static List<GameObject> mapObjects= new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static void Start()
    {
        GenerateRoads();

    }

    // Update is called once per frame
    void Update()
    {

    }
    private static void GenerateRoads()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                
                
                    GameObject roadTile = getRoadTile(new Vector3(i, 0, j), j%3 != 0 && i== 2);
                    mapObjects.Add(roadTile);
                
                
            }
        }
    }
    static GameObject getRoadTile(Vector3 pos, bool isWhite)
    {
        GameObject roadTile = new GameObject("Road Tile");
        roadTile.transform.position = pos;
        MeshFilter filter = roadTile.AddComponent<MeshFilter>();
        MeshRenderer renderer= roadTile.AddComponent<MeshRenderer>();
        Mesh mesh = new Mesh();
        Vector3[] verticies = new Vector3[4]
        {
            new Vector3 (0, 0, 0)
            ,
            new Vector3(1, 0, 0)
            ,
            new Vector3(1, 0, 1)
            ,
            new Vector3(0, 0, 1)
        };

        int[] triangles = new int[6] { 0, 2, 1, 0, 3, 2 };
        mesh.vertices = verticies;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        filter.mesh= mesh;

        Material mat = new Material(Shader.Find("Unlit/Color"));
        if(isWhite)
            mat.color = Color.white;
        else mat.color = Color.black;
        renderer.material = mat;

      



        return roadTile;
    }
}
