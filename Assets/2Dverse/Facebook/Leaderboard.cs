using System.Collections.Generic;
using Facebook.Unity;
using GraphQL;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour {

	public GameObject LeaderboardPanel;
	public GameObject LeaderboardItemPrefab;
	public ScrollRect LeaderboardScrollRect;

	public class Score {

		public int score;
		public Dictionary<string, string> user;

	}

	private string query =
		@"query {
			GetScores {
				score
				user {
					id
				}
			}
		}";

	private List<Score> scores;

	private void OnEnable () {
		EraseUI();
		
		APIGraphQL.Query(query, null, result => {
			scores = result.Get<List<Score>>("GetScores");
			DrawUI();
		});
	}

	private void EraseUI () {
		Transform[] childLBElements = LeaderboardPanel.GetComponentsInChildren<Transform>();
		foreach (Transform childObject in childLBElements) {
			if (!LeaderboardPanel.transform.IsChildOf(childObject.transform)) {
				Destroy(childObject.gameObject);
			}
		}
	}

	private void DrawUI () {
		
		// Populate leaderboard
		for (int i = 0; i < scores.Count; i++) {
			GameObject LBgameObject = Instantiate(LeaderboardItemPrefab);
			LeaderBoardElement LBelement = LBgameObject.GetComponent<LeaderBoardElement>();
			LBelement.SetupElement(i + 1, scores[i]);
			LBelement.transform.SetParent(LeaderboardPanel.transform, false);
		}

		// Scroll to top
		LeaderboardScrollRect.verticalNormalizedPosition = 1f;
	}

}