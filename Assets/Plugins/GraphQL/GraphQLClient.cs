﻿using System;
using System.Collections;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
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

		private UnityWebRequest QueryRequest (string query, object variables, string token = null) {
			var fullQuery = new GraphQLQuery() {
				query = query,
				variables = variables,
			};

			string json = JsonConvert.SerializeObject(fullQuery, Formatting.None);
			UnityWebRequest request = UnityWebRequest.Post(url, UnityWebRequest.kHttpVerbPOST);

			byte[] payload = Encoding.UTF8.GetBytes(Convert.ToBase64String(RC4.Encrypt(json)));
			request.uploadHandler = new UploadHandlerRaw(payload);
			request.SetRequestHeader("Content-Type", "application/twodversestudio.custom-type");
			
			if (token != null) request.SetRequestHeader("Authorization", "Bearer " + token);

			return request;
		}

		private IEnumerator SendRequest (string query, object variables = null,
			Action<GraphQLResponse> callback = null,
			string token = null) {
			var request = QueryRequest(query, variables, token);

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

		public void Query (string query, object variables = null, Action<GraphQLResponse> callback = null,
			string sToken = "") {
			Coroutiner.StartCoroutine(SendRequest(query, variables, callback, sToken));
		}

	}
}