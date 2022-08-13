using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Architecture;

public class Grave : BaseMonoBehaviour
{
    public GraveAttribute graveAttribute;

    private void Start()
    {
        SetGrave();
    }

    public void SetGrave()
    {
        graveAttribute.health = Random.Range(50, 100);
        graveAttribute.money = Random.Range(50, 100);
        int treasureCount = Random.Range(0, 4);
        //for (int i = 0; i < treasureCount; i++)
        //{
        //    // Пока рандомные, потом нужно сделать будет определенные
        //    Treasure localTreasure = new Treasure();
        //    localTreasure.name = "treasure" + i.ToString();
        //    graveAttribute.treasures.Add(localTreasure);
        //}
    }
}

[System.Serializable]
public class GraveAttribute
{
    public float health;
    public int money;
    public List<Treasure> treasures = new List<Treasure>();
}
