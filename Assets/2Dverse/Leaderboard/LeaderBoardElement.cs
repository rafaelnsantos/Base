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

	public void SetupElement (int rank, Rank score) {
		Rank.text = rank.ToString() + ".";
		Score.text = "Smashed: " + score.score;

		string userid = score.id;
		string username;
		if (FacebookCache.ScoreNames.TryGetValue(userid, out username)) {
			Name.text = username;
		} else {
			FB.API(userid + "?fields=first_name", HttpMethod.GET, res => {
				if (res.Error != null) {
					return;
				}

				if (res.ResultDictionary.TryGetValue("first_name", out username)) {
					Name.text = username;
					FacebookCache.ScoreNames.Add(userid, username);
				}
			});
		}

		Texture picture;
		if (FacebookCache.ScoreImages.TryGetValue(userid, out picture)) {
			ProfilePicture.texture = picture;
		} else {
			GraphUtil.LoadImgFromID(userid, pictureTexture => {
				FacebookCache.ScoreImages.Add(userid, pictureTexture);
				ProfilePicture.texture = pictureTexture;
			});
		}

	}

}