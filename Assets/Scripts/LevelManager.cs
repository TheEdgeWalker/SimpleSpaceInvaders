using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
	[SerializeField] private LevelData levelData;
	[SerializeField] private WaveController waveController;
	[SerializeField] private UnityEvent onWin;
	[SerializeField] private UnityEvent onLose;
	[SerializeField] private UnityEvent onEnemyDied;

	private Dictionary<GameObject, GameObjectPool> enemyPool = new Dictionary<GameObject, GameObjectPool>();

	private int currenWaveIndex = 0;
	private Wave currentWave;

	private void Awake()
	{
		Assert.IsNotNull(levelData);
		Assert.IsNotNull(waveController);

		InitEnemyPool();
		InitWave(currenWaveIndex);
	}

	private void InitEnemyPool()
	{
		Dictionary<GameObject, int> totalEnemies = new Dictionary<GameObject, int>();

		foreach (WaveData wave in levelData.Waves)
		{
			Dictionary<GameObject, int> waveEnemies = new Dictionary<GameObject, int>();
			foreach (ColumnData column in wave.columns)
			{
				foreach (EnemyData enemyData in column.enemies)
				{
					if (waveEnemies.ContainsKey(enemyData.prefab))
					{
						waveEnemies[enemyData.prefab]++;
					}
					else
					{
						waveEnemies[enemyData.prefab] = 1;
					}
				}
			}

			foreach (var waveEnemy in waveEnemies)
			{
				if (totalEnemies.ContainsKey(waveEnemy.Key))
				{
					if (waveEnemy.Value > totalEnemies[waveEnemy.Key])
					{
						totalEnemies[waveEnemy.Key] = waveEnemy.Value;
					}
				}
				else
				{
					totalEnemies[waveEnemy.Key] = waveEnemy.Value;
				}
			}
		}

		foreach (var prefabCount in totalEnemies)
		{
			enemyPool[prefabCount.Key] = new GameObjectPool(prefabCount.Key, prefabCount.Value, transform);
		}
	}

	private GameObject GetFromEnemyPool(GameObject prefab)
	{
		GameObjectPool pool;
		if (enemyPool.TryGetValue(prefab, out pool))
		{
			return pool.GetAvailable();
		}

		return null;
	}

	private void InitWave(int index)
	{
		WaveData waveData = levelData.Waves[index];

		List<Column> columns = new List<Column>(waveData.columns.Length);

		foreach (ColumnData columnData in waveData.columns)
		{
			Column column = new Column(columnData);

			foreach (EnemyData enemyData in columnData.enemies)
			{
				GameObject enemy = GetFromEnemyPool(enemyData.prefab);
				if (enemy != null)
				{
					enemy.transform.localPosition = enemyData.position;
					enemy.SetActive(true);
					enemy.SendMessage("OnInitWave", SendMessageOptions.DontRequireReceiver);

					column.AddEnemy(enemy);
				}
				else
				{
					Debug.LogWarning("Enemy not available: " + enemyData.prefab.name);
				}
			}

			columns.Add(column);
		}

		currentWave = new Wave(waveData, columns);

		waveController.StartWave(currentWave);
	}

	private void OnEnemyDied(GameObject enemy)
	{
		onEnemyDied.Invoke();

		currentWave.EnemyDied(enemy);

		if (currentWave.IsCleared())
		{
			if (++currenWaveIndex < levelData.Waves.Length)
			{
				InitWave(currenWaveIndex);
			}
			else
			{
				waveController.StopWave();
				onWin.Invoke();
			}
		}
	}

	private void OnWallColliderBottom()
	{
		waveController.StopWave();
		onLose.Invoke();
	}

	public void OnPlayerDeath()
	{
		waveController.StopWave();
		onLose.Invoke();
	}
}
