using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	[SerializeField] private Image imageBackground;
	[SerializeField] private Image imageForeground;

	public void Init()
	{
		imageForeground.rectTransform.offsetMax = new Vector2(0f, 0f);
	}

	public void SetCurrent(int defaultHP, int currentHP)
	{
		float diff = defaultHP - currentHP;
		float offset = diff / (float)defaultHP * imageBackground.rectTransform.sizeDelta.x;

		imageForeground.rectTransform.offsetMax = new Vector2(-offset, 0f);
	}
}
