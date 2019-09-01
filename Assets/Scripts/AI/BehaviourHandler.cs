using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourHandler
{
	private Dictionary<string, Behaviour> behaviours = new Dictionary<string, Behaviour>();
	private Behaviour currentBehaviour;

	private Dictionary<string, object> parameters = new Dictionary<string, object>();
	private Dictionary<string, float> cooldowns = new Dictionary<string, float>();

	public void AddBehaviours(Behaviour behaviour)
	{
		behaviours[behaviour.name] = behaviour;
	}

	public void Update()
	{
		if (currentBehaviour != null)
			currentBehaviour.Update();
	}

	public void ShiftBehaviour(string name)
	{
		Debug.Log("ShiftBehaviour to: " + name);

		if (currentBehaviour != null)
			currentBehaviour.End();

		currentBehaviour = behaviours[name];

		currentBehaviour.Start();
	}

	public void SetParameter(string name, object value)
	{
		parameters[name] = value;
	}

	public object GetParameter(string name)
	{
		object value;
		parameters.TryGetValue(name, out value);
		return value;
	}

	public void StartCooldown(string name, float duration)
	{
		cooldowns[name] = Time.time + duration;
	}

	public bool IsNotInCooldown(string name)
	{
		float time;
		if (cooldowns.TryGetValue(name, out time))
		{
			return time <= Time.time;
		}

		return true;
	}
}
