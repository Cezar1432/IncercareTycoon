using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CoffeTable1 : MonoBehaviour
{

    [SerializeField] GameObject table;
    [SerializeField] GameObject chair;
    [SerializeField]int tilesFromTable = 2;
    public void spawn(Vector3 tablePos)
    {
        GameObject coffeeTable= Instantiate(table, tablePos, Quaternion.identity);
        GameObject chair1 = Instantiate(chair, new Vector3(tablePos.x + tilesFromTable, tablePos.y, tablePos.z), Quaternion.Euler(0, 270, 0));
        GameObject chair2 = Instantiate(chair, new Vector3(tablePos.x, tablePos.y, tablePos.z+ tilesFromTable), Quaternion.Euler(0, 180, 0));
        GameObject chair3 = Instantiate(chair, new Vector3(tablePos.x- tilesFromTable, tablePos.y, tablePos.z), Quaternion.Euler(0, 90, 0));
        GameObject chair4 = Instantiate(chair, new Vector3(tablePos.x, tablePos.y, tablePos.z- tilesFromTable), Quaternion.Euler(0, 360, 0));
        List<GameObject> objs= new List<GameObject> { coffeeTable, chair1, chair2, chair3, chair4 };
        foreach (GameObject obj in objs)
        {
            obj.transform.localScale = Vector3.one * 0.54f;

        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
}
