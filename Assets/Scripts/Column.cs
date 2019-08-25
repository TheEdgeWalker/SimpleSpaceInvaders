using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Column
{
	private readonly List<GameObject> enemies;

	public List<GameObject> Enemies { get { return enemies; } }

	public Column(ColumnData data)
	{
		enemies = new List<GameObject>(data.enemies.Length);
	}

	public void AddEnemy(GameObject enemy)
	{
		enemies.Add(enemy);
	}

	public bool IsActive()
	{
		return enemies.Any(go => go.activeInHierarchy);
	}

	public Vector3 GetFirePosition()
	{
		Vector3 position = Vector3.zero;

		for (int i = 0; i < enemies.Count; ++i)
		{
			GameObject enemy = enemies[i];

			if (i == 0)
				position = enemy.transform.position;

			if (enemy.transform.position.y < position.y && enemy.activeInHierarchy)
			{
				position = enemy.transform.position;
			}
		}

		return position;
	}
}
