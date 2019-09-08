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

	private Dictionary<string, GameObjectPool> bullets = new Dictionary<string, GameObjectPool>();

	public static BulletManager Instance { get; private set; }

	private void Awake()
	{
		Assert.IsNotNull(datas);
		Assert.IsTrue(datas.Length > 0);

		foreach (BulletData data in datas)
		{
			bullets[data.prefab.name] = new GameObjectPool(data.prefab.gameObject, data.cacheCount, transform);
		}

		Instance = this;
	}

	public void Fire(string name, Vector3 origin, float angle)
	{
		GameObjectPool pool;
		if (bullets.TryGetValue(name, out pool))
		{
			GameObject bullet = pool.GetAvailable();
			if (bullet != null)
			{
				bullet.transform.position = origin;
				bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);
			}
		}
		else
		{
			Debug.LogWarning("Tried to fire unknown bullet: " + name);
		}
	}
}
