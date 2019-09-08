using UnityEngine;

public class GameObjectPool
{
	private GameObject[] pool;
	private GameObject prefab;

	private int currentIndex = 0;

	public GameObjectPool(GameObject prefab, int count, Transform parent)
	{
		pool = new GameObject[count];
		this.prefab = prefab;

		for (int i = 0; i < count; ++i)
		{
			GameObject gameObject = GameObject.Instantiate(prefab.gameObject, parent);
			gameObject.SetActive(false);
			pool[i] = gameObject;
		}
	}

	public GameObject GetAvailable()
	{
		int originalIndex = currentIndex;

		do
		{
			GameObject go = pool[currentIndex];
			if (!go.activeInHierarchy)
			{
				go.SetActive(true);
				return go;
			}
			else
			{
				if (currentIndex == pool.Length - 1)
					currentIndex = 0;
				else
					currentIndex++;
			}
		} while (currentIndex != originalIndex);

		Debug.LogWarning("Cannot find available instance: " + prefab.name);
		return null;
	}
}
