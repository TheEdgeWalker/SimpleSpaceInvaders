using UnityEngine;

public class HitPoints
{
	public delegate void HitPointChanged(int defaultHP, int currentHP);
	private HitPointChanged OnChanged;

	private readonly int defaultValue;
	private int currentValue;

	public int DefaultValue { get { return defaultValue; } }
	public int CurrentValue { get { return currentValue; } }

	public HitPoints(int defaultValue)
	{
		currentValue = this.defaultValue = defaultValue;
	}

	public void SubscribeOnChanged(HitPointChanged hitPointChanged)
	{
		OnChanged += hitPointChanged;
	}

	public void UnsubscribeOnChanged(HitPointChanged hitPointChanged)
	{
		OnChanged -= hitPointChanged;
	}

	public bool IsAlive()
	{
		return currentValue > 0;
	}

	public void Damage(int damage)
	{
		currentValue = Mathf.Max(currentValue - damage, 0);

		OnChanged?.Invoke(defaultValue, currentValue);
	}

	public void Reset()
	{
		currentValue = defaultValue;

		OnChanged?.Invoke(defaultValue, currentValue);
	}
}
