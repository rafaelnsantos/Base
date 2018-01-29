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

	public static void SetInt (string key, int value, Action<GraphQLResponse> callback = null) {
		if (FB.IsLoggedIn) APIGraphQL.Query(mutationInteger, new {key, value}, callback);
	}

	public static void GetInt (string key, Action<int> callback) {
		if (FB.IsLoggedIn) APIGraphQL.Query(queryInteger, new {key}, result => callback(result.Get<int>("GetInt")));
	}

	public static void SetString (string key, string value, Action<GraphQLResponse> callback = null) {
		if (FB.IsLoggedIn) APIGraphQL.Query(mutationString, new {key, value}, callback);
	}

	public static void GetString (string key, Action<string> callback) {
		if (FB.IsLoggedIn) APIGraphQL.Query(queryString, new {key}, result => callback(result.Get<string>("GetString")));
	}

	public static void SetBool (string key, bool value, Action<GraphQLResponse> callback = null) {
		if (FB.IsLoggedIn) APIGraphQL.Query(mutationBoolean, new {key, value}, callback);
	}

	public static void GetBool (string key, Action<bool> callback) {
		if (FB.IsLoggedIn) APIGraphQL.Query(queryBoolean, new {key}, result => callback(result.Get<bool>("GetBool")));
	}

	public static void SetFloat (string key, float value, Action<GraphQLResponse> callback = null) {
		if (FB.IsLoggedIn) APIGraphQL.Query(mutationFloat, new {key, value}, callback);
	}

	public static void GetFloat (string key, Action<float> callback) {
		if (FB.IsLoggedIn) APIGraphQL.Query(queryFloat, new {key}, result => callback(result.Get<float>("GetFloat")));
	}

}