using System.Collections.Generic;
using Facebook.Unity;
using GraphQL;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour {

	public GameObject LeaderboardPanel;
	public GameObject LeaderboardItemPrefab;
	public ScrollRect LeaderboardScrollRect;
	
	private string query =
		@"query {
			Leaderboard {
				score
				user {
					id
				}
			}
		}";

	private List<Rank> scores;

	private void OnEnable () {		
		API.Query(query, null, result => {
			scores = result.Get<List<Rank>>("Leaderboard");
			DrawUI();
		});
	}

	private void DrawUI () {
		Transform[] childLBElements = LeaderboardPanel.GetComponentsInChildren<Transform>();
		foreach (Transform childObject in childLBElements) {
			if (!LeaderboardPanel.transform.IsChildOf(childObject.transform)) {
				Destroy(childObject.gameObject);
			}
		}
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

public class Rank {

	public int score;
	public User user;

}

public class User {

	public string id;
	public string name;

}