using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine;
using UnityEngine.UI;

public class AchievementHolder : MonoBehaviour {

	public GameObject AchievementPanel;
	public GameObject AchievementItemPrefab;
	public ScrollRect AchievementScrollRect;

	private void OnEnable () {
		FBAchievements.GetAchievements(RedrawUI);
	}

	public void RedrawUI () {
		// Clear out previous leaderboard
		Transform[] childLBElements = AchievementPanel.GetComponentsInChildren<Transform>();
		foreach (Transform childObject in childLBElements) {
			if (!AchievementPanel.transform.IsChildOf(childObject.transform)) {
				Destroy(childObject.gameObject);
			}
		}
		
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

}