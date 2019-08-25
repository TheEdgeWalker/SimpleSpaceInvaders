using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMessageSender : MonoBehaviour
{
	[SerializeField] private string methodName;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		collision.SendMessageUpwards(methodName, SendMessageOptions.DontRequireReceiver);
	}
}
