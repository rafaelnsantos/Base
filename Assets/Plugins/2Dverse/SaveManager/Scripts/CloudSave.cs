using System;
using Facebook.Unity;
using GraphQL;

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

	public static void SetInt (string key, int value, Action<bool> callback) {
		API.Query(mutationInteger, new {key, value}, saved => callback(saved.Get<bool>("SetInt")));
	}
	
	public static void GetInt (string key, Action<int> callback) {
		API.Query(queryInteger, new {key}, result => callback(result.Get<int>("GetInt")));
	}

	public static void SetString (string key, string value, Action<bool> callback) {
		API.Query(mutationString, new {key, value}, saved => callback(saved.Get<bool>("SetInt")));
	}

	public static void GetString (string key, Action<string> callback) {
		API.Query(queryString, new {key}, result => callback(result.Get<string>("GetString")));
	}

	public static void SetBool (string key, bool value, Action<bool> callback) {
		API.Query(mutationBoolean, new {key, value}, saved => callback(saved.Get<bool>("SetInt")));
	}

	public static void GetBool (string key, Action<bool> callback) {
		API.Query(queryBoolean, new {key}, result => callback(result.Get<bool>("GetBool")));
	}

	public static void SetFloat (string key, float value, Action<bool> callback) {
		API.Query(mutationFloat, new {key, value}, saved => callback(saved.Get<bool>("SetInt")));
	}

	public static void GetFloat (string key, Action<float> callback) {
		API.Query(queryFloat, new {key}, result => callback(result.Get<float>("GetFloat")));
	}

}