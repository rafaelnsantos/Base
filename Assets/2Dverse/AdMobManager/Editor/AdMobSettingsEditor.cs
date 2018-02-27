using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AdMobSettings))]
public class AdMobSettingsEditor : Editor {

	[MenuItem("2Dverse/Edit AdMob Settings")]
	public static void Edit () {
		var instance = AdMobSettings.NullableInstance;

		if (instance == null) {
			instance = CreateInstance<AdMobSettings>();
			string properPath = Path.Combine(Application.dataPath, AdMobSettings.Path);
			if (!Directory.Exists(properPath)) {
				Directory.CreateDirectory(properPath);
			}

			string fullPath = Path.Combine(
				Path.Combine("Assets", AdMobSettings.Path),
				AdMobSettings.AssetName + AdMobSettings.AssetExtension);
			AssetDatabase.CreateAsset(instance, fullPath);
		}

		Selection.activeObject = AdMobSettings.Instance;
	}

	public override void OnInspectorGUI () {
		AdMobSettings.Teste = !EditorGUILayout.BeginToggleGroup("Production?", AdMobSettings.Teste);
		AdMobSettings.NoAds = EditorGUILayout.Toggle("NoAds?", AdMobSettings.NoAds);
		AdMobSettings.AndroidAppId = EditorGUILayout.TextField("AndroidAppId", AdMobSettings.AndroidAppId);
		AdMobSettings.IphoneAppId = EditorGUILayout.TextField("IphoneAppId", AdMobSettings.IphoneAppId);
		AdMobSettings.TagForChild = EditorGUILayout.Toggle("Tag for Child", AdMobSettings.TagForChild);
		EditorGUILayout.EndToggleGroup();

		if (GUI.changed) EditorUtility.SetDirty((AdMobSettings) target);
	}

}