using UnityEngine;

[System.Serializable]
public struct ColumnData
{
	public GameObject[] enemies;
}

[CreateAssetMenu]
public class WaveData : ScriptableObject
{
	[SerializeField] private Vector2 spacing;
	[SerializeField] private ColumnData[] columns;
	[SerializeField] private float speed;
	[SerializeField] private float fireCooldown;
	[SerializeField] private Vector2 startingPosition;

	public Vector2 Spacing { get { return spacing; } }
	public ColumnData[] Columns { get { return columns; } }
	public float Speed { get { return speed; } }
	public float FireCooldown { get { return fireCooldown; } }
	public Vector2 StartingPosition { get { return startingPosition; } }
}
