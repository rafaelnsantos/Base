using System;
using UnityEngine;

namespace GraphQL {
	public static class API {

		private static readonly GraphQLClient client = new GraphQLClient(CloudSaveSettings.URL);

		public static void Query (string query, object variables = null, Action<GraphQLResponse> callback = null) {
			client.Query(query, variables, callback);
		}

	}
}