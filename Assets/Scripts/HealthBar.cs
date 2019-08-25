using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	[SerializeField] private Image imageBackground;
	[SerializeField] private Image imageForeground;

	int defaultHP = 0;

	public void Init(int defaultHP)
	{
		imageForeground.rectTransform.offsetMax = new Vector2(0f, 0f);
		this.defaultHP = defaultHP;
	}

	public void SetCurrent(int currentHP)
	{
		float diff = defaultHP - currentHP;
		float offset = diff / (float)defaultHP * imageBackground.rectTransform.sizeDelta.x;

		imageForeground.rectTransform.offsetMax = new Vector2(-offset, 0f);
	}
}
