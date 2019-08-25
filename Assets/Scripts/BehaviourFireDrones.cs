using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourFireDrones : Behaviour
{
	public BehaviourFireDrones(BehaviourHandler handler) : base("FireDrones", handler)
	{
	}

	protected override void OnStart()
	{
		Transform transform = (Transform)handler.GetParameter("transform");

		BulletManager.Instance.Fire("Drone", transform.position, 0f);
		BulletManager.Instance.Fire("Drone", transform.position - new Vector3(1f, 0), 0f);
		BulletManager.Instance.Fire("Drone", transform.position + new Vector3(1f, 0), 0f);

		handler.StartCooldown("FireDrones", 10f);

		ShiftBehaviourAfterWait("Patrol", 2f);
	}

	protected override void OnUpdate()
	{
	}

	protected override void OnEnd()
	{
	}
}
