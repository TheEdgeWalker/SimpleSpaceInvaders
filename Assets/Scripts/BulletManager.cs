using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[System.Serializable]
public struct BulletData
{
	public Bullet prefab;
	public int cacheCount;
}

public class BulletManager : MonoBehaviour
{
	[SerializeField] private BulletData[] datas;

	private Dictionary<string, GameObject[]> bullets = new Dictionary<string, GameObject[]>();

	public static BulletManager Instance { get; private set; }

	private void Awake()
	{
		Assert.IsNotNull(datas);
		Assert.IsTrue(datas.Length > 0);

		foreach (BulletData data in datas)
		{
			bullets[data.prefab.name] = new GameObject[data.cacheCount];

			for (int i = 0; i < data.cacheCount; ++i)
			{
				GameObject bullet = Instantiate(data.prefab.gameObject, transform);
				bullet.SetActive(false);
				bullets[data.prefab.name][i] = bullet;
			}
		}

		Instance = this;
	}

	public void Fire(string name, Vector3 origin, float angle)
	{
		GameObject[] pool;
		if (bullets.TryGetValue(name, out pool))
		{
			foreach (GameObject bullet in pool)
			{
				if (!bullet.activeInHierarchy)
				{
					bullet.transform.position = origin;
					bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);
					bullet.SetActive(true);
					return;
				}
			}

			Debug.LogWarning("No available bullets: " + name);
		}
		else
		{
			Debug.LogWarning("Tried to fire unknown bullet: " + name);
		}
	}
}
