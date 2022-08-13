using System.Collections.Generic;
using UnityEngine;
using Project.Architecture;

public class Grave : BaseMonoBehaviour
{
	[SerializeField] private GraveAttribute _graveAttribute;

	private void Start()
	{
		SetGrave();
	}

	protected void SetGrave()
	{
		_graveAttribute.health = Random.Range(50, 100);
		_graveAttribute.money = Random.Range(50, 100);
		int treasureCount = Random.Range(0, 4);
		//for (int i = 0; i < treasureCount; i++)
		//{
		//    // Пока рандомные, потом нужно сделать будет определенные
		//    Treasure localTreasure = new Treasure();
		//    localTreasure.name = "treasure" + i.ToString();
		//    _graveAttribute.treasures.Add(localTreasure);
		//}
	}

	public void TakeDamage(float damage)
	{
		_graveAttribute.health -= damage;
		print(_graveAttribute.health);
		if (!(_graveAttribute.health <= 0))
			return;
		print("Grave excavated");
		Destroy(gameObject);
	}
}

[System.Serializable]
public class GraveAttribute
{
	public float health;
	public int money;
	public List<Treasure> treasures = new();
}
