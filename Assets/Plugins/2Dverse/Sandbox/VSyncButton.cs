using UnityEngine;
using UnityEngine.UI;

public class VSyncButton : Savable {

	public Text text;

	private void Start () {
		Load();
		GetComponent<Button>().onClick.AddListener(Switch);
		SetText();
	}

	private void Switch () {
		QualitySettings.vSyncCount = QualitySettings.vSyncCount == 1 ? 2 : 1;
		SetText();
		Save();
	}

	protected override void Save () {
		SaveManager.SetInt("vsync", QualitySettings.vSyncCount);
	}

	protected override void Load () {
		QualitySettings.vSyncCount = SaveManager.GetInt("vsync", 1);
	}

	private void SetText () {
		int refreshRate = Screen.currentResolution.refreshRate / QualitySettings.vSyncCount;
		text.text = refreshRate.ToString();
	}

}