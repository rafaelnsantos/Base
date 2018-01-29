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

}