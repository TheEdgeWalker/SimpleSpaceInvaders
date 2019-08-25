using UnityEngine;

public class Enemy : MonoBehaviour
{
	public void OnDeath()
	{
		SendMessageUpwards("OnEnemyDied", gameObject, SendMessageOptions.DontRequireReceiver);
	}
}
