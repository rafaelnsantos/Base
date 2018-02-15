using UnityEditor;
using UnityEngine;

public class APIEditor : EditorWindow {

	[MenuItem("CloudSave/Edit Settings")]
	static void Init () {
		EditorWindow window = GetWindow(typeof(APIEditor));
		window.name = "CloudSave Settings";
		window.Show();
	}

	public void OnGUI () {
		
		SaveManager.SetString("url", EditorGUILayout.TextField("URL", SaveManager.GetString("url")));

		SaveManager.SetBool("encrypt", EditorGUILayout.BeginToggleGroup("Encryption", SaveManager.GetBool("encrypt")));
		SaveManager.SetString("key", EditorGUILayout.PasswordField("Encryption key", SaveManager.GetString("key")));
		EditorGUILayout.EndToggleGroup();
	}
	
	[MenuItem("CloudSave/Developers Page")]
	static void Page () {
		Application.OpenURL("https://github.com/rafaelnsantos/unity-cloudsave");
	}
	
	[MenuItem("CloudSave/Report a Bug")]
	static void BugPage () {
		Application.OpenURL("https://github.com/rafaelnsantos/unity-cloudsave/issues");
	}
	
	

}