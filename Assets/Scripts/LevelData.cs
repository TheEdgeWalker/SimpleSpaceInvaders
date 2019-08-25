using UnityEngine;

[CreateAssetMenu]
public class LevelData : ScriptableObject
{
	[SerializeField] private WaveData[] waves;

	public WaveData[] Waves { get { return waves; } }
}
