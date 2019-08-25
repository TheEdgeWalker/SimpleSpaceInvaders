using UnityEngine;

public class HitPoints
{
	private readonly int defaultValue;
	private int currentValue;

	public int DefaultValue { get { return defaultValue; } }
	public int CurrentValue { get { return currentValue; } }

	public HitPoints(int defaultValue)
	{
		currentValue = this.defaultValue = defaultValue;
	}

	public bool IsAlive()
	{
		return currentValue > 0;
	}

	public void Damage(int damage)
	{
		currentValue = Mathf.Max(currentValue - damage, 0);
	}
}
