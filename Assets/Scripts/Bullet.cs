using UnityEngine;
using UnityEngine.Assertions;

public class Bullet : MonoBehaviour
{
	[SerializeField] int damage;
	[SerializeField] protected float speed;

	private void Update()
	{
		Move();

		if (Mathf.Abs(transform.position.x) > 10f || Mathf.Abs(transform.position.y) > 10f)
		{
			gameObject.SetActive(false);
		}
	}

	protected virtual void Move()
	{
		transform.position += transform.up * speed * Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.layer == 0 || collider.gameObject.layer == gameObject.layer)
			return;

		collider.gameObject.SendMessage("OnHit", damage, SendMessageOptions.DontRequireReceiver);
		gameObject.SetActive(false);
	}
}
