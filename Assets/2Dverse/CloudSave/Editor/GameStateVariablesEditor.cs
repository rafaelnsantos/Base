using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameStateVariables))]
public class GameStateVariablesEditor : Editor {

	[MenuItem("Memory Cloud/Edit Variables")]
	public static void Edit () {
		var instance = GameStateVariables.NullableInstance;

		if (instance == null) {
			instance = CreateInstance<GameStateVariables>();
			string properPath = Path.Combine(Application.dataPath, GameStateVariables.Path);
			if (!Directory.Exists(properPath)) {
				Directory.CreateDirectory(properPath);
			}

			string fullPath = Path.Combine(
				Path.Combine("Assets", GameStateVariables.Path),
				GameStateVariables.AssetName + GameStateVariables.AssetExtension);
			AssetDatabase.CreateAsset(instance, fullPath);
		}

		Selection.activeObject = GameStateVariables.Instance;
	}

	public override void OnInspectorGUI () {
		DrawDefaultInspector();
		
		if (GUI.changed) EditorUtility.SetDirty((GameStateVariables) target);
	}

}