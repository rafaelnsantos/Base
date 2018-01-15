using SmartLocalization;
using UnityEngine;

public class RandomText : TextController {

	public int Quantity;

	private void OnEnable () {
		string key = string.Concat(Key, (int) UnityEngine.Random.Range(0f, Quantity));
		Text.text = LanguageManager.Instance.GetTextValue(key);
	}

	private void Start () {}

}