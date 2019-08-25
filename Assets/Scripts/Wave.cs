using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave
{
	private readonly WaveData waveData;
	private readonly List<Column> columns;
	private readonly HashSet<GameObject> enemies = new HashSet<GameObject>();

	public WaveData WaveData { get { return waveData; } }

	public Wave(WaveData waveData, List<Column> columns)
	{
		this.waveData = waveData;
		this.columns = columns;

		foreach (Column column in columns)
		{
			foreach (GameObject enemy in column.Enemies)
			{
				enemies.Add(enemy);
			}
		}
	}

	public void EnemyDied(GameObject enemy)
	{
		if (!enemies.Remove(enemy))
			Debug.LogError("Tried to kill an unknown enemy: " + enemy.name);
	}

	public bool IsCleared()
	{
		return enemies.Count == 0;
	}

	public Vector3? GetFirePosition()
	{
		List<Column> availableColumns = columns.FindAll(column => column.IsActive());
		if (availableColumns.Count == 0)
			return null;

		Column chosenColumn = availableColumns[Random.Range(0, availableColumns.Count)];
		return chosenColumn.GetFirePosition();
	}
}
