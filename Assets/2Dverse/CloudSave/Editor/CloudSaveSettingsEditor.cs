using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CloudSaveSettings))]
public class CloudSaveSettingsEditor : Editor {

	[MenuItem("Memory Cloud/Edit Settings")]
	public static void Edit () {
		var instance = CloudSaveSettings.NullableInstance;

		if (instance == null) {
			instance = CreateInstance<CloudSaveSettings>();
			string properPath = Path.Combine(Application.dataPath, CloudSaveSettings.Path);
			if (!Directory.Exists(properPath)) {
				Directory.CreateDirectory(properPath);
			}

			string fullPath = Path.Combine(
				Path.Combine("Assets", CloudSaveSettings.Path),
				CloudSaveSettings.AssetName + CloudSaveSettings.AssetExtension);
			AssetDatabase.CreateAsset(instance, fullPath);
		}

		Selection.activeObject = CloudSaveSettings.Instance;
	}

	public override void OnInspectorGUI () {
		CloudSaveSettings.URL = EditorGUILayout.TextField("URL", CloudSaveSettings.URL);
		CloudSaveSettings.Encrypted = EditorGUILayout.BeginToggleGroup("Encrypted?", CloudSaveSettings.Encrypted);
		CloudSaveSettings.Key = EditorGUILayout.PasswordField("Encryption key", CloudSaveSettings.Key);
		EditorGUILayout.EndToggleGroup();
		if (GUI.changed) EditorUtility.SetDirty((CloudSaveSettings) target);
	}

	[MenuItem("Memory Cloud/Developers Page")]
	static void Page () {
		Application.OpenURL("https://github.com/memory-cloud");
	}

	[MenuItem("Memory Cloud/Report a Bug")]
	static void BugPage () {
		Application.OpenURL("https://github.com/memory-cloud/docs/issues");
	}

}