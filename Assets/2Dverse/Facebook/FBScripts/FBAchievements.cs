using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine;

public static class FBAchievements {

	public static void GiveAchievement (string achievementUrl) {
		var data = new Dictionary<string, string>() {{"achievement", achievementUrl}};
		FB.API("/me/achievements",
			HttpMethod.POST,
			AchievementCallback,
			data);
	}

	private static void AchievementCallback (IGraphResult result) {
		if (result != null) {
			// Handle result
			Debug.Log(result.RawResult);
			FBGraph.GetPlayerInfo();
		}
	}

}