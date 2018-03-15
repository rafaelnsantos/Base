using System;
using System.Collections;
using Facebook.Unity;
using GraphQL;
using UnityEngine;

public static class CloudSave {

	private static string mutationInteger =
		@"mutation($key: String!, $value: Int!) {
            SetInt(
                key: $key
                value: $value
            )
        }";

	private static string queryInteger =
		@"query($key: String!) {
            GetInt(
                key: $key
            )
        }";

	private static string mutationString =
		@"mutation($key: String!, $value: String!) {
            SetString(
                key: $key
                value: $value
            )
        }";

	private static string queryString =
		@"query($key: String!) {
            GetString(
                key: $key
            )
        }";

	private static string mutationBoolean =
		@"mutation($key: String!, $value: Boolean!) {
            SetBool(
                key: $key
                value: $value
            )
        }";

	private static string queryBoolean =
		@"query($key: String!) {
            GetBool(
                key: $key
            )
        }";

	private static string mutationFloat =
		@"mutation($key: String!, $value: Float!) {
            SetFloat(
                key: $key
                value: $value
            )
        }";

	private static string queryFloat =
		@"query($key: String!) {
            GetFloat(
                key: $key
            )
        }";

	private static string queryCheck =
		@"query {
			Check
		}";

	private static void CheckInternetConnection (Action<bool> action) {
		if (!FB.IsLoggedIn || Application.internetReachability == NetworkReachability.NotReachable) {
			action(false);
			return;
		}

		API.Query(queryCheck, null, result => action(result.Get<bool>("Check")));
	}

	public static void SetInt (string key, int value) {
		API.Query(mutationInteger, new {key, value});
		SaveManager.SetInt(key, value);
	}

	public static void GetInt (string key, Action<int> callback) {
		CheckInternetConnection(connected => {
			if (connected) {
				API.Query(queryInteger, new {key}, result => callback(result.Get<int>("GetInt")));
			} else {
				callback(SaveManager.GetInt(key));
			}
		});
	}

	public static void SetString (string key, string value) {
		API.Query(mutationString, new {key, value});
		SaveManager.SetString(key, value);
	}

	public static void GetString (string key, Action<string> callback) {
		CheckInternetConnection(connected => {
			if (connected) {
				API.Query(queryString, new {key}, result => callback(result.Get<string>("GetString")));
			} else {
				callback(SaveManager.GetString(key));
			}
		});
	}

	public static void SetBool (string key, bool value) {
		API.Query(mutationBoolean, new {key, value});
		SaveManager.SetBool(key, value);
	}

	public static void GetBool (string key, Action<bool> callback) {
		CheckInternetConnection(connected => {
			if (connected) {
				API.Query(queryBoolean, new {key}, result => callback(result.Get<bool>("GetBool")));
			} else {
				callback(SaveManager.GetBool(key));
			}
		});
	}

	public static void SetFloat (string key, float value) {
		API.Query(mutationFloat, new {key, value});
		SaveManager.SetFloat(key, value);
	}

	public static void GetFloat (string key, Action<float> callback) {
		CheckInternetConnection(connected => {
			if (connected) {
				API.Query(queryFloat, new {key}, result => callback(result.Get<float>("GetFloat")));
			} else {
				callback(SaveManager.GetFloat(key));
			}
		});
	}

}