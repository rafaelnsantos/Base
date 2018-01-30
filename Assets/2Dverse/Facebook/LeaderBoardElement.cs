using Facebook.Unity;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardElement : MonoBehaviour {

	//   Element references (Set in Unity Editor)   //
//	public Image Background;
	public RawImage ProfilePicture;

	public Text Rank;
	public Text Name;
	public Text Score;

	public void SetupElement (int rank, Leaderboard.Score score) {
		Rank.text = rank.ToString() + ".";
		Score.text = "Smashed: " + score.score;

		string playerName;
		string id;

		if (!score.user.TryGetValue("id", out id)) return;

		if (FacebookCache.ScoreNames.TryGetValue(id, out playerName)) {
			Name.text = playerName;
		} else {
			FB.API(id + "?fields=first_name", HttpMethod.GET, result => {
				if (result.Error != null) {
					return;
				}

				if (result.ResultDictionary.TryGetValue("first_name", out playerName)) {
					Name.text = playerName;
					FacebookCache.ScoreNames.Add(id, playerName);
				}
			});
		}

		Texture picture;
		if (FacebookCache.ScoreImages.TryGetValue(id, out picture)) {
			ProfilePicture.texture = picture;
		} else {
			GraphUtil.LoadImgFromID(id, pictureTexture => {
				FacebookCache.ScoreImages.Add(id, pictureTexture);
				ProfilePicture.texture = pictureTexture;
			});
		}
	}

}