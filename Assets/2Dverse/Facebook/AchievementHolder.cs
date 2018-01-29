using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine;
using UnityEngine.UI;

public class AchievementHolder : MonoBehaviour {

	public GameObject AchievementPanel;
	public GameObject AchievementItemPrefab;
	public ScrollRect AchievementScrollRect;

	private void OnEnable () {
		if (!FB.IsLoggedIn) return;

		if (FacebookCache.Achievements == null) {
			FB.API("/app/achievements?fields=title,description", HttpMethod.GET, GetAchievementsCallback);
		}
		else {
			EraseUI();
			RedrawUI();
		}
	}

	public void EraseUI () {
		// Clear out previous leaderboard
		Transform[] childLBElements = AchievementPanel.GetComponentsInChildren<Transform>();
		foreach (Transform childObject in childLBElements) {
			if (!AchievementPanel.transform.IsChildOf(childObject.transform)) {
				Destroy(childObject.gameObject);
			}
		}
	}

	public void RedrawUI () {
		EraseUI();
		var achievements = FacebookCache.Achievements;

		if (achievements.Count <= 0) {
			return;
		}

		// Populate leaderboard
		foreach (object achievement in achievements) {
			GameObject LBgameObject = Instantiate(AchievementItemPrefab);
			AchievementElement LBelement = LBgameObject.GetComponent<AchievementElement>();
			LBelement.SetupElement(achievement);
			LBelement.transform.SetParent(AchievementPanel.transform, false);
			if (LBelement.Completed) {
				LBelement.transform.SetAsFirstSibling();
			} else {
				LBelement.transform.SetAsLastSibling();
			}
		}

		// Scroll to top
		AchievementScrollRect.verticalNormalizedPosition = 1f;
	}

	private void GetAchievementsCallback (IGraphResult result) {
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

			structuredAchievements.Add(entry);
		}

		FacebookCache.Achievements = structuredAchievements;

		// Redraw the UI
		RedrawUI();
	}

}