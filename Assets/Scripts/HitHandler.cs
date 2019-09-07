using UnityEngine;
using UnityEngine.Events;

public class HitHandler : MonoBehaviour
{
	[SerializeField] private int defaultHP;
	[SerializeField] private HealthBar healthBar;
	[SerializeField] private UnityEvent onHandleDeath;

	private HitPoints hp;

	public void OnInitWave()
	{
		hp.Reset();
	}

	private void Awake()
	{
		hp = new HitPoints(defaultHP);

		if (healthBar != null)
		{
			healthBar.Init();
			hp.SubscribeOnChanged(healthBar.SetCurrent);
		}
	}

	private void OnHit(int damage)
	{
		hp.Damage(damage);

		if (!hp.IsAlive())
		{
			gameObject.SetActive(false);
			onHandleDeath.Invoke();
		}
	}
}
