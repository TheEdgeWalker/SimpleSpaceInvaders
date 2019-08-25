using UnityEngine;

public class Drone : Bullet
{
	[SerializeField] private float finalSpeed;
	[SerializeField] private float detectRadius;

	protected override void Move()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectRadius, LayerMask.GetMask("Player"));
		foreach (Collider2D collider in colliders)
		{
			if (collider != null && collider.tag == "Player")
			{
				Vector3 direction = (collider.transform.position - transform.position).normalized;
				transform.position += direction * finalSpeed * Time.deltaTime;
				return;
			}
		}

		transform.position += new Vector3(0f, -speed * Time.deltaTime);
	}
}
