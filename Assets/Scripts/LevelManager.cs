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

	private Dictionary<GameObject, GameObject[]> enemyPool = new Dictionary<GameObject, GameObject[]>();

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
			foreach (ColumnData column in wave.Columns)
			{
				foreach (GameObject prefab in column.enemies)
				{
					if (waveEnemies.ContainsKey(prefab))
					{
						waveEnemies[prefab]++;
					}
					else
					{
						waveEnemies[prefab] = 1;
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
			enemyPool[prefabCount.Key] = new GameObject[prefabCount.Value];

			for (int i = 0; i < prefabCount.Value; ++i)
			{
				GameObject go = Instantiate(prefabCount.Key, transform);
				go.SetActive(false);

				enemyPool[prefabCount.Key][i] = go;
			}
		}
	}

	private GameObject GetFromEnemyPool(GameObject prefab)
	{
		GameObject[] pool;
		if (enemyPool.TryGetValue(prefab, out pool))
		{
			foreach (GameObject instance in pool)
			{
				if (!instance.activeInHierarchy)
				{
					return instance;
				}
			}
		}

		return null;
	}

	private void InitWave(int index)
	{
		WaveData waveData = levelData.Waves[index];

		List<Column> columns = new List<Column>(waveData.Columns.Length);

		float x = 0f;
		foreach (ColumnData columnData in waveData.Columns)
		{
			float y = 0f;
			Column column = new Column(columnData);

			foreach (GameObject prefab in columnData.enemies)
			{
				GameObject enemy = GetFromEnemyPool(prefab);
				enemy.transform.localPosition = new Vector3(x, y);
				enemy.SetActive(true);

				column.AddEnemy(enemy);

				y -= waveData.Spacing.y;
			}

			x += waveData.Spacing.x;

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
