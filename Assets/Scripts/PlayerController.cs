using System.Threading.Tasks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float speed;
	[SerializeField] private float fireCooldown;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Fire();
		}

		float move = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		if ((move > 0f && transform.position.x < 6f) ||
			(move < 0f && transform.position.x > -6f))
		{
			transform.position += new Vector3(move, 0f);
		}
	}

	private bool cooldown = false;
	private async void Fire()
	{
		if (cooldown)
			return;

		BulletManager.Instance.Fire("PlayerBullet", transform.position, 0f);
		cooldown = true;

		await Task.Delay(System.TimeSpan.FromSeconds(fireCooldown));
		cooldown = false;
	}
}
