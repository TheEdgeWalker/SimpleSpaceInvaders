using UnityEngine;
using UnityEngine.UI;

public class TextScore : MonoBehaviour
{
	[SerializeField] private Text text;

	private int score = 0;

	public void OnEnemyDied()
	{
		score += 100;
		text.text = score.ToString();
	}
}
