using System;

namespace GraphQL {
	public static class APIGraphQL {
	
		 private static readonly GraphQLClient API = new GraphQLClient("https://unity-cloudsave-development.herokuapp.com/graphql");
//		private static readonly GraphQLClient API = new GraphQLClient("https://unity-cloudsave.herokuapp.com/graphql");
		// private static readonly GraphQLClient API = new GraphQLClient("http://192.168.0.104:3000/graphql");
		// private static readonly GraphQLClient API = new GraphQLClient("http://192.168.99.100:3000/graphql");
		// private static readonly GraphQLClient API = new GraphQLClient("http://cloudsave.openode.io/graphql");

		public static void Query (string query, object variables = null, Action<GraphQLResponse> callback = null) {
			API.Query(query, variables, callback);
		}

	}
}