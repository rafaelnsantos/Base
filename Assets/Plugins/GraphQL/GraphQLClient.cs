﻿using System;
using System.Collections;
using System.Text;
using Facebook.Unity;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace GraphQL {
	public class GraphQLClient {

		private string url;

		public GraphQLClient (string url) {
			this.url = url;
		}

		private class GraphQLQuery {

			public string query;
			public object variables;

		}

		private UnityWebRequest QueryRequest (string query, object variables) {
			var fullQuery = new GraphQLQuery() {
				query = query,
				variables = variables,
			};

			string json = JsonConvert.SerializeObject(fullQuery, Formatting.None);
			UnityWebRequest request = UnityWebRequest.Post(url, UnityWebRequest.kHttpVerbPOST);

			byte[] payload = Encoding.UTF8.GetBytes(Convert.ToBase64String(RC4.Encrypt(json)));
			request.uploadHandler = new UploadHandlerRaw(payload);
			request.SetRequestHeader("Content-Type", "application/twodversestudio.custom-type");

			request.SetRequestHeader("Authorization", "Bearer " + AccessToken.CurrentAccessToken.TokenString);
			request.SetRequestHeader("AppId", FB.AppId);

			return request;
		}

		private IEnumerator SendRequest (string query, object variables = null, Action<GraphQLResponse> callback = null) {
			var request = QueryRequest(query, variables);

			using (UnityWebRequest www = request) {
				yield return www.SendWebRequest();

				if (www.isNetworkError) {
					if (callback != null) callback(new GraphQLResponse("", www.error));
					yield break;
				}

				string responseString = www.downloadHandler.text;

				responseString = RC4.Decrypt(Convert.FromBase64String(responseString));

				var result = new GraphQLResponse(responseString);

				if (callback != null) callback(result);
			}

			request.Dispose();
		}

		public void Query (string query, object variables = null, Action<GraphQLResponse> callback = null) {
			Coroutiner.StartCoroutine(SendRequest(query, variables, callback));
		}

	}
}