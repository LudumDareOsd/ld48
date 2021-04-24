using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
	public TextMeshProUGUI score;

	public void Awake() {
		score.text = "100";
	}
}
