using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
	private float direction = 1f;

	private readonly BehaviourHandler behaviourHandler = new BehaviourHandler();

	private void Awake()
	{
		behaviourHandler.SetParameter("transform", transform);
		behaviourHandler.SetParameter("direction", 1f);

		behaviourHandler.AddBehaviours(new BehaviourPatrol(behaviourHandler));
		behaviourHandler.AddBehaviours(new BehaviourFireSalvo(behaviourHandler));
		behaviourHandler.AddBehaviours(new BehaviourFireDrones(behaviourHandler));

		behaviourHandler.ShiftBehaviour("Patrol");
		behaviourHandler.StartCooldown("FireDrones", 30f);
	}

	private void Update()
	{
		behaviourHandler.Update();
	}

	private void OnWallColliderLeft()
	{
		behaviourHandler.SetParameter("direction", 1f);
	}

	private void OnWallColliderRight()
	{
		behaviourHandler.SetParameter("direction", -1f);
	}
}
