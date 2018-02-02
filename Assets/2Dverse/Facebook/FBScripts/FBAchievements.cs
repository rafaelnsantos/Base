using System;
using System.Collections.Generic;
using Facebook.Unity;

public static class FBAchievements {

	public class Achievement {

		public string id;
		public string title;
		public string description;

	}

	public static void GiveAchievement (string achievementUrl) {
		if (FBLogin.HavePublishActions) {
			var data = new Dictionary<string, string>() {{"achievement", achievementUrl}};
			FB.API("/me/achievements",
				HttpMethod.POST,
				AchievementCallback,
				data);
		} else {
			FBLogin.PromptForPublish(delegate {
				if (FBLogin.HavePublishActions) {
					GiveAchievement(achievementUrl);
				}
			});
		}
	}

	private static void AchievementCallback (IGraphResult result) {
		if (result.Error != null) {
			return;
		}

		GetCompletedAchievements();
	}

	public static void GetCompletedAchievements () {
		string queryString = "/me?fields=achievements{data{achievement{id}},end_time}";
		FB.API(queryString, HttpMethod.GET, GetCompletedAchievementsCallback);
	}

	private static void GetCompletedAchievementsCallback (IGraphResult result) {
		if (result.Error != null) {
			return;
		}

		FacebookCache.CompletedAchievements = GraphUtil.DeserializeAchievements(result.ResultDictionary);
	}

	public static void GetAchievements (Action callback = null) {
		if (FacebookCache.Achievements != null) {
			if (callback != null) callback();
			return;
		}
		FB.API("/app/achievements?fields=title,description", HttpMethod.GET, result => {
			if (result.Error != null) {
				return;
			}

			// Parse scores info
			var achievementsList = new List<object>();

			object achievementsh;
			if (result.ResultDictionary.TryGetValue("data", out achievementsh)) {
				achievementsList = (List<object>) achievementsh;
			}

			// Parse score data
			var structuredAchievements = new List<object>();
			foreach (object achievementItem in achievementsList) {
				var entry = (Dictionary<string, object>) achievementItem;

				string achievementID = (string) entry["id"];
				
				GraphUtil.LoadImgFromID(achievementID, pictureTexture => {
					if (pictureTexture != null) {
						FacebookCache.AchievementsImages.Add(achievementID, pictureTexture);
					}
				});
				

				structuredAchievements.Add(entry);
			}

			FacebookCache.Achievements = structuredAchievements;

			if (callback != null) callback();
		});
	}
	
}