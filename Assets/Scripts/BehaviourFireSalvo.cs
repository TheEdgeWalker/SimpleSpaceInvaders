using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourFireSalvo : Behaviour
{
	public BehaviourFireSalvo(BehaviourHandler handler) : base("FireSalvo", handler)
	{
	}

	protected override void OnStart()
	{
		Transform transform = (Transform)handler.GetParameter("transform");

		BulletManager.Instance.Fire("BossBullet", transform.position, 165f);
		BulletManager.Instance.Fire("BossBullet", transform.position, 175f);
		BulletManager.Instance.Fire("BossBullet", transform.position, 185f);
		BulletManager.Instance.Fire("BossBullet", transform.position, 195f);

		ShiftBehaviourAfterWait("Patrol", 1f);
	}

	protected override void OnUpdate()
	{
	}

	protected override void OnEnd()
	{
	}
}
