using System;
using UnityEngine;

namespace GraphQL {
	public static class API {

		public static string url = SaveManager.GetString("url");
		public static bool encrypt = SaveManager.GetBool("encrypt");
		public static string key = SaveManager.GetString("key");
		private static readonly GraphQLClient client = new GraphQLClient(url);

		public static void Query (string query, object variables = null, Action<GraphQLResponse> callback = null) {
			client.Query(query, variables, callback);
		}

	}
}