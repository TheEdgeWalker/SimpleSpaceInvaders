using UnityEngine;
using UnityEngine.Events;

public class HitHandler : MonoBehaviour
{
	[SerializeField] private int defaultHP;
	[SerializeField] private HealthBar healthBar;
	[SerializeField] private UnityEvent onHandleDeath;

	private HitPoints hp;

	private void Awake()
	{
		hp = new HitPoints(defaultHP);

		if (healthBar != null)
			healthBar.Init(defaultHP);
	}

	private void OnHit(int damage)
	{
		hp.Damage(damage);

		if (healthBar != null)
			healthBar.SetCurrent(hp.CurrentValue);

		if (!hp.IsAlive())
		{
			gameObject.SetActive(false);
			onHandleDeath.Invoke();
		}
	}
}
