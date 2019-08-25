using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[SerializeField] private Text textWin;
	[SerializeField] private Text textLose;

	public void OnWin()
	{
		textWin.gameObject.SetActive(true);
	}

	public void OnLose()
	{
		textLose.gameObject.SetActive(true);
	}
}
