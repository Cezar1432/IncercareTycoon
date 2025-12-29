using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.SpeedTree.Importer;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] NPCPrefabs = new GameObject[18];
    [SerializeField] Material[] palettes= new Material[9];
    [SerializeField] Texture2D[] textures = new Texture2D[9];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    public float spawnInterval = 10, lastSpawn= 0;
    public float setSpawnInterval()
    {
        return this.lastSpawn;
    }
    List<NPC> npcs= new List<NPC>();

    // Update is called once per frame
    public void Update()
    {
        lastSpawn += Time.deltaTime;
        if (lastSpawn > spawnInterval)
        {
            lastSpawn = 0;
            spawnNPC();
        }
    }
    Vector3 spawnPose = new Vector3();
    Quaternion spawnRotation = Quaternion.identity;
    void spawnNPC()
    {
        GameObject randomNPC= NPCPrefabs[Random.Range(0, NPCPrefabs.Length)];
        int spawn = Random.Range(0, 2);
        switch (spawn)
        {
            case 0:
                spawnPose = new Vector3(6.5f, 0, 50f);
                spawnRotation = Quaternion.identity;
                break;
            case 1:
                spawnPose = new Vector3(6.5f, 0, 0f);
                spawnRotation = Quaternion.Euler(0, 180, 0);
                break;

        }
        GameObject npc= Instantiate(randomNPC, spawnPose, spawnRotation);
        npc.transform.localScale = Vector3.one * 1.5f;
        SkinnedMeshRenderer renderer = npc.GetComponentInChildren<SkinnedMeshRenderer>();
        if (renderer != null && palettes.Length > 0)
        {
            Material mat = new Material(palettes[Random.Range(0, palettes.Length)]); // unique copy
            Shader urp = Shader.Find("Universal Render Pipeline/Lit");
            if (urp != null) mat.shader = urp;

            // Built-in fallback:
            if (mat.shader == null)
            {
                Shader std = Shader.Find("Standard");
                if (std != null) mat.shader = std;
            }

            if (textures != null && textures.Length > 0)
            {
                Texture2D tex = textures[Random.Range(0, textures.Length)];
                if (tex != null)
                {
                    if (mat.HasProperty("_BaseMap")) mat.SetTexture("_BaseMap", tex);   // URP Lit
                    else if (mat.HasProperty("_MainTex")) mat.SetTexture("_MainTex", tex);   // Built-in/Standard
                }
            }

            renderer.material = mat;
        }
        npc.AddComponent<NPC>();
     
    }
}
