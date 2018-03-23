using System.Collections.Generic;
using Facebook.Unity;
using GraphQL;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour {

	public GameObject LeaderboardPanel;
	public GameObject LeaderboardItemPrefab;
	public ScrollRect LeaderboardScrollRect;
	public string Key = "score";

	public string QueryLeaderboard () {
		string query = "query ($" + Key + ": String!) {\n";
		query += "\tLeaderboard(key: $" + Key + ") { leaderboard { id score } position } \n}";
		return query;
	}

	private Lista scores;

	private void OnEnable () {
		JObject variables = new JObject();
		variables[Key] = Key;
		API.Query(QueryLeaderboard(), variables, result => {
			scores = result.Get<Lista>("Leaderboard");

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
		for (int i = 0; i < scores.leaderboard.Count; i++) {
			GameObject LBgameObject = Instantiate(LeaderboardItemPrefab);
			LeaderBoardElement LBelement = LBgameObject.GetComponent<LeaderBoardElement>();
			LBelement.SetupElement(i + 1, scores.leaderboard[i]);
			LBelement.transform.SetParent(LeaderboardPanel.transform, false);
		}

		// Scroll to top
		LeaderboardScrollRect.verticalNormalizedPosition = 1f;
	}

}

struct Lista {

	public List<Rank> leaderboard;
	public int position;

}

public struct Rank {

	public int score;
	public string id;

}