using UnityEngine;

public class StartGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public CarSpawner carSpawner;
    [SerializeField] public NPCSpawner npcSpawner;
    void Start()
    {
        MapGeneration.GenerateFirstFloor();
        
    }

    // Update is called once per frame
    void Update()
    {
        carSpawner.UpdateCarBehaviour();
        npcSpawner.Update();
    }
}
