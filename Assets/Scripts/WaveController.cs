using System.Threading.Tasks;
using UnityEngine;

public class WaveController : MonoBehaviour
{
	private const float DownDistance = 0.6f;

	private Wave currentWave;

	private float direction = 1f;
	private float down = 0f;

	public void StartWave(Wave wave)
	{
		currentWave = wave;

		transform.position = wave.WaveData.StartingPosition;
	}

	public void StopWave()
	{
		currentWave = null;
	}

	private void Update()
	{
		if (currentWave == null)
			return;

		if (down > 0f)
		{
			float y = Mathf.Min(currentWave.WaveData.Speed * Time.deltaTime, down);
			transform.position += new Vector3(0, -y);
			down -= y;
		}
		else
		{
			float x = currentWave.WaveData.Speed * direction * Time.deltaTime;
			transform.position += new Vector3(x, 0);
		}

		Fire();
	}

	private bool cooldown = false;
	private async void Fire()
	{
		if (cooldown || currentWave.WaveData.FireCooldown == 0f)
			return;

		Vector3? position = currentWave.GetFirePosition();
		if (!position.HasValue)
			return;

		BulletManager.Instance.Fire("EnemyBullet", currentWave.GetFirePosition().Value, 180f);

		cooldown = true;

		await Task.Delay(System.TimeSpan.FromSeconds(currentWave.WaveData.FireCooldown));
		cooldown = false;
	}

	private void OnWallColliderLeft()
	{
		direction = 1f;

		if (down <= 0f)
			down = DownDistance;
	}

	private void OnWallColliderRight()
	{
		direction = -1f;

		if (down <= 0f)
			down = DownDistance;
	}
}
