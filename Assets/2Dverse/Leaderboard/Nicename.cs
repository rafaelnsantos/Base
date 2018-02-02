using System;
using System.Collections.Generic;
using Facebook.Unity;
using GraphQL;
using UnityEngine;

public class Rank {

	public int score;
	public User user;

}

public class User {

	public string id;
	public string name;
	public Texture picture;

}

public static class Nicename {

	public static List<Rank> Scores;

	private static string query =
		@"query {
			Leaderboard {
				score
				user {
					id
				}
			}
		}";

	public static void GetScores (Action callback = null) {
		APIGraphQL.Query(query, null, result => {
			Scores = result.Get<List<Rank>>("Leaderboard");

			foreach (var score in Scores) {
				string name;
				User user = score.user;
				if (FacebookCache.ScoreNames.TryGetValue(user.id, out name)) {
					user.name = name;
				} else {
					FB.API(user.id + "?fields=first_name", HttpMethod.GET, res => {
						if (res.Error != null) {
							return;
						}

						if (res.ResultDictionary.TryGetValue("first_name", out name)) {
							user.name = name;
							FacebookCache.ScoreNames.Add(user.id, name);
						}
					});
				}

				Texture picture;
				if (FacebookCache.ScoreImages.TryGetValue(user.id, out picture)) {
					user.picture = picture;
				} else {
					GraphUtil.LoadImgFromID(user.id, pictureTexture => {
						FacebookCache.ScoreImages.Add(user.id, pictureTexture);
						user.picture = pictureTexture;
					});
				}
			}

			if (callback != null) callback();
		});
	}

}