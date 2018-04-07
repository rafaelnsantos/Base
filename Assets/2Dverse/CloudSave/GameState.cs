using System;
using System.Collections.Generic;
using System.Linq;
using GraphQL;
using Newtonsoft.Json.Linq;
using UnityEngine;

public static class GameState {

	private static Dictionary<string, string> Strings;
	private static Dictionary<string, int> Integers;
	private static Dictionary<string, float> Floats;
	private static Dictionary<string, bool> Booleans;

	public static Action onLoad;
	
	static GameState () {
		Strings = new Dictionary<string, string>();
		Integers = new Dictionary<string, int>();
		Floats = new Dictionary<string, float>();
		Booleans = new Dictionary<string, bool>();

		foreach (var VAR in GameStateVariables.StringKeys) {
			Strings.Add(VAR, "");
		}
		foreach (var VAR in GameStateVariables.IntKeys) {
			Integers.Add(VAR, 0);
		}
		foreach (var VAR in GameStateVariables.FloatKeys) {
			Floats.Add(VAR, 0f);
		}
		foreach (var VAR in GameStateVariables.BoolKeys) {
			Booleans.Add(VAR, false);
		}
	}

	public static bool NeedSave { get; private set; }

	public static void SetInt (string key, int value) {
		Integers[key] = value;
		NeedSave = true;
	}

	public static int GetInt (string key) {
		return Integers[key];
	}

	public static void SetString (string key, string value) {
		Strings[key] = value;
		NeedSave = true;
	}

	public static string GetString (string key) {
		return Strings[key];
	}

	public static void SetFloat (string key, float value) {
		Floats[key] = value;
		NeedSave = true;
	}

	public static float GetFloat (string key) {
		return Floats[key];
	}

	public static void SetBool (string key, bool value) {
		Booleans[key] = value;
		NeedSave = true;
	}

	public static bool GetBool (string key) {
		return Booleans[key];
	}

	public static void Save () {
		if (!NeedSave && !LocalAhead) return;

        SaveOnline(success => {
	        if (success) NeedSave = false;
        });

		SaveOffline();
		NeedSave = false;
	}

	public static void Load () {
		LoadOffline();

//		if (LocalAhead) {
//			SaveOnline();
//			return;
//		}

		JObject variables = new JObject();
		foreach (var VAR in Strings) {
			variables["string" + VAR.Key] = VAR.Key;
		}

		foreach (var VAR in Integers) {
			variables["int" + VAR.Key] = VAR.Key;
		}

		foreach (var VAR in Floats) {
			variables["float" + VAR.Key] = VAR.Key;
		}

		foreach (var VAR in Booleans) {
			variables["bool" + VAR.Key] = VAR.Key;
		}

		API.Query(QueryBuilder(), variables, callback => {
			if (callback.Exception != null) {
				if (onLoad != null) onLoad();
				return;
			}

			foreach (var key in GameStateVariables.StringKeys) {
				Strings[key] = callback.Get<string>("string" + key);
			}

			foreach (var key in GameStateVariables.IntKeys) {
				Integers[key] = callback.Get<int>("int" + key);
			}

			foreach (var key in GameStateVariables.FloatKeys) {
				Floats[key] = callback.Get<float>("float" + key);
			}

			foreach (var key in GameStateVariables.BoolKeys) {
				Booleans[key] = callback.Get<bool>("bool" + key);
			}

			if (onLoad != null) onLoad();
		});
	}

	private static void SaveOnline (Action<bool> finished = null) {
		JObject variables = new JObject();
		foreach (var VAR in Strings) {
			variables["string" + VAR.Key] = VAR.Key;
			variables["string" + VAR.Key + "value"] = VAR.Value;
		}

		foreach (var VAR in Integers) {
			variables["int" + VAR.Key] = VAR.Key;
			variables["int" + VAR.Key + "value"] = VAR.Value;
		}

		foreach (var VAR in Floats) {
			variables["float" + VAR.Key] = VAR.Key;
			variables["float" + VAR.Key + "value"] = VAR.Value;
		}

		foreach (var VAR in Booleans) {
			variables["bool" + VAR.Key] = VAR.Key;
			variables["bool" + VAR.Key + "value"] = VAR.Value;
		}

		API.Query(MutationBuilder(), variables, callback => {
			if (callback.HasError) {
				// not saved online
				Debug.Log(callback.Exception);
				if (finished != null) finished(false);
				return;
			}

			SaveManager.SetDateTime("LastSavedOnline", DateTime.Now);
			if (finished != null) finished(true);
		});
	}

	private static void SaveOffline () {
		foreach (var VAR in Strings) {
			SaveManager.SetString(VAR.Key, VAR.Value);
		}

		foreach (var VAR in Integers) {
			SaveManager.SetInt(VAR.Key, VAR.Value);
		}

		foreach (var VAR in Floats) {
			SaveManager.SetFloat(VAR.Key, VAR.Value);
		}

		foreach (var VAR in Booleans) {
			SaveManager.SetBool(VAR.Key, VAR.Value);
		}

		SaveManager.SetDateTime("LastSavedLocal", DateTime.Now);
	}

	private static void LoadOffline () {
		foreach (var key in GameStateVariables.StringKeys) {
			Strings[key] = SaveManager.GetString(key);
		}

		foreach (var key in GameStateVariables.IntKeys) {
			Integers[key] = SaveManager.GetInt(key);
		}

		foreach (var key in GameStateVariables.FloatKeys) {
			Floats[key] = SaveManager.GetFloat(key);
		}

		foreach (var key in GameStateVariables.BoolKeys) {
			Booleans[key] = SaveManager.GetBool(key);
		}
		if (onLoad != null) onLoad();
	}

	public static string QueryBuilder () {
		string query = "query (";

		bool hasBool = Booleans.Count > 0;
		bool hasInt = Integers.Count > 0;
		bool hasFloat = Floats.Count > 0;

		foreach (var VAR in Strings) {
			query += "$string" + VAR.Key + ": String!";
			if (!hasInt && !hasFloat && !hasBool && Strings.Last().Equals(VAR)) {
				query += ") {\n";
			} else {
				query += ", ";
			}
		}

		foreach (var VAR in Integers) {
			query += "$int" + VAR.Key + ": String!";
			if (!hasFloat && !hasBool && Integers.Last().Equals(VAR)) {
				query += ") {\n";
			} else {
				query += ", ";
			}
		}

		foreach (var VAR in Floats) {
			query += "$float" + VAR.Key + ": String!";
			if (!hasBool && Floats.Last().Equals(VAR)) {
				query += ") {\n";
			} else {
				query += ", ";
			}
		}

		foreach (var VAR in Booleans) {
			query += "$bool" + VAR.Key + ": String!";
			if (Booleans.Last().Equals(VAR)) {
				query += ") {\n";
			} else {
				query += ", ";
			}
		}

		foreach (var VAR in Strings) {
			query += "\tstring" + VAR.Key + ": GetString(key: $string" + VAR.Key + ")\n";
			if (!hasInt && !hasFloat && !hasBool && Strings.Last().Equals(VAR)) {
				query += "}";
			}
		}

		foreach (var VAR in Integers) {
			query += "\tint" + VAR.Key + ": GetInt(key: $int" + VAR.Key + ")\n";
			if (!hasFloat && !hasBool && Integers.Last().Equals(VAR)) {
				query += "}";
			}
		}

		foreach (var VAR in Floats) {
			query += "\tfloat" + VAR.Key + ": GetFloat(key: $float" + VAR.Key + ")\n";
			if (!hasBool && Floats.Last().Equals(VAR)) {
				query += "}";
			}
		}

		foreach (var VAR in Booleans) {
			query += "\tbool" + VAR.Key + ": GetBool(key: $bool" + VAR.Key + ")\n";
			if (Booleans.Last().Equals(VAR)) {
				query += "}";
			}
		}

		return query;
	}

	public static string MutationBuilder () {
		string mutation = "mutation (";

		bool hasBool = Booleans.Count > 0;
		bool hasInt = Integers.Count > 0;
		bool hasFloat = Floats.Count > 0;

		foreach (var VAR in Strings) {
			mutation += "$string" + VAR.Key + ": String!, ";
			mutation += "$string" + VAR.Key + "value" + ": String!";
			if (!hasInt && !hasFloat && !hasBool && Strings.Last().Equals(VAR)) {
				mutation += ") {\n";
			} else {
				mutation += ", ";
			}
		}

		foreach (var VAR in Integers) {
			mutation += "$int" + VAR.Key + ": String!, ";
			mutation += "$int" + VAR.Key + "value" + ": Int!";
			if (!hasFloat && !hasBool && Integers.Last().Equals(VAR)) {
				mutation += ") {\n";
			} else {
				mutation += ", ";
			}
		}

		foreach (var VAR in Floats) {
			mutation += "$float" + VAR.Key + ": String!, ";
			mutation += "$float" + VAR.Key + "value" + ": Float!";
			if (!hasBool && Floats.Last().Equals(VAR)) {
				mutation += ") {\n";
			} else {
				mutation += ", ";
			}
		}

		foreach (var VAR in Booleans) {
			mutation += "$bool" + VAR.Key + ": String!, ";
			mutation += "$bool" + VAR.Key + "value" + ": Boolean!";
			if (Booleans.Last().Equals(VAR)) {
				mutation += ") {\n";
			} else {
				mutation += ", ";
			}
		}

		foreach (var VAR in Strings) {
			mutation += "\tstring" + VAR.Key + ": SetString(key: $string" + VAR.Key + ", value: $string" + VAR.Key + "value)\n";
		}

		foreach (var VAR in Integers) {
			mutation += "\tint" + VAR.Key + ": SetInt(key: $int" + VAR.Key + ", value: $int" + VAR.Key + "value)\n";
		}

		foreach (var VAR in Floats) {
			mutation += "\tfloat" + VAR.Key + ": SetFloat(key: $float" + VAR.Key + ", value: $float" + VAR.Key + "value)\n";
		}

		foreach (var VAR in Booleans) {
			mutation += "\tbool" + VAR.Key + ": SetBool(key: $bool" + VAR.Key + ", value: $bool" + VAR.Key + "value)\n";
		}

		mutation += "}";

		return mutation;
	}

	private static bool LocalAhead {
		get { return SaveManager.GetDateTime("LastSavedOnline").CompareTo(SaveManager.GetDateTime("LastSavedLocal")) == -1; }
	}

}