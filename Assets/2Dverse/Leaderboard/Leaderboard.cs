using System.Collections.Generic;
using Facebook.Unity;
using GraphQL;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour {

	public GameObject LeaderboardPanel;
	public GameObject LeaderboardItemPrefab;
	public ScrollRect LeaderboardScrollRect;

	private void OnEnable () {		
		Nicename.GetScores(DrawUI);
	}

	private void DrawUI () {
		Transform[] childLBElements = LeaderboardPanel.GetComponentsInChildren<Transform>();
		foreach (Transform childObject in childLBElements) {
			if (!LeaderboardPanel.transform.IsChildOf(childObject.transform)) {
				Destroy(childObject.gameObject);
			}
		}
		// Populate leaderboard
		for (int i = 0; i < Nicename.Scores.Count; i++) {
			GameObject LBgameObject = Instantiate(LeaderboardItemPrefab);
			LeaderBoardElement LBelement = LBgameObject.GetComponent<LeaderBoardElement>();
			LBelement.SetupElement(i + 1, Nicename.Scores[i]);
			LBelement.transform.SetParent(LeaderboardPanel.transform, false);
		}

		// Scroll to top
		LeaderboardScrollRect.verticalNormalizedPosition = 1f;
	}

}