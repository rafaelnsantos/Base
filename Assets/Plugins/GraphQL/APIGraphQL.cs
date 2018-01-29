﻿using System;

namespace GraphQL {
	public static class APIGraphQL {

		private const string ApiURL = "https://unity-cloudsave.herokuapp.com/graphql";
		public static string Token = null;

		public static bool LoggedIn {
			get { return !Token.Equals(""); } //todo: improve loggedin verification
		}

//		private static readonly GraphQLClient API = new GraphQLClient("http://localhost:3000/graphql");
		private static readonly GraphQLClient API = new GraphQLClient(ApiURL);

		public static void Query (string query, object variables = null, Action<GraphQLResponse> callback = null) {
			API.Query(query, variables, callback, Token);
		}

	}
}