using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourPatrol : Behaviour
{
	public BehaviourPatrol(BehaviourHandler handler) : base("Patrol", handler)
	{
	}

	protected override void OnStart()
	{
	}

	protected override void OnUpdate()
	{
		if (handler.IsNotInCooldown("FireDrones"))
		{
			handler.ShiftBehaviour("FireDrones");
		}
		else
		{
			Transform transform = (Transform)handler.GetParameter("transform");
			float direction = (float)handler.GetParameter("direction");

			transform.position += new Vector3(3f * direction * Time.deltaTime, 0);

			RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 15f, LayerMask.GetMask("Player"));
			foreach (RaycastHit2D hit in hits)
			{
				if (hit.collider != null && hit.collider.tag == "Player")
				{
					handler.ShiftBehaviour("FireSalvo");
				}
			}
		}
	}

	protected override void OnEnd()
	{
	}
}
