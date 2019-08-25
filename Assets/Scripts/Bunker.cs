using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunker : MonoBehaviour
{
	private void OnHit(int damage)
	{
		gameObject.SetActive(false);
	}
}
