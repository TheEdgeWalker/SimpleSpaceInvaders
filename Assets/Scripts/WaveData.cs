using UnityEngine;

[System.Serializable]
public struct EnemyData
{
	public GameObject prefab;
	public Vector2 position;
}

[System.Serializable]
public struct ColumnData
{
	public EnemyData[] enemies;
}

[CreateAssetMenu]
public class WaveData : ScriptableObject
{
	public ColumnData[] columns;
	public float speed;
	public float fireCooldown;
	public Vector2 startingPosition;
}
