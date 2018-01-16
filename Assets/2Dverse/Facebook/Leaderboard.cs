using Facebook.Unity;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour {

	public GameObject LeaderboardPanel;
	public GameObject LeaderboardItemPrefab;
	public ScrollRect LeaderboardScrollRect;

	private void OnEnable () {
		if (!FB.IsLoggedIn) return;

		FBGraph.GetScores();
	}

	public void RedrawUI () {
		var scores = FacebookInfo.Scores;
		
		if (scores.Count <= 0) {
			return;
		}
		
		// Clear out previous leaderboard
		Transform[] childLBElements = LeaderboardPanel.GetComponentsInChildren<Transform>();
		foreach (Transform childObject in childLBElements) {
			if (!LeaderboardPanel.transform.IsChildOf(childObject.transform)) {
				Destroy(childObject.gameObject);
			}
		}

		// Populate leaderboard
		for (int i = 0; i < scores.Count; i++) {
			GameObject LBgameObject = Instantiate(LeaderboardItemPrefab) as GameObject;
			LeaderBoardElement LBelement = LBgameObject.GetComponent<LeaderBoardElement>();
			LBelement.SetupElement(i + 1, scores[i]);
			LBelement.transform.SetParent(LeaderboardPanel.transform, false);
		}

		// Scroll to top
		LeaderboardScrollRect.verticalNormalizedPosition = 1f;
	}

}