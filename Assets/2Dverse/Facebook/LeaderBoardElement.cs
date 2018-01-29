using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LeaderBoardElement : MonoBehaviour {

	//   Element references (Set in Unity Editor)   //
//	public Image Background;
	public RawImage ProfilePicture;

	public Text Rank;
	public Text Name;
	public Text Score;

	public void SetupElement (int rank, Leaderboard.Score score) {
		Rank.text = rank.ToString() + ".";
		Name.text = score.name.Split(' ')[0];
		Score.text = "Smashed: " + score.score;

		Texture picture;
		if (FacebookCache.ScoreImages.TryGetValue((string) score.id, out picture)) {
			ProfilePicture.texture = picture;
		} else {
			GraphUtil.LoadImgFromID(score.id, pictureTexture => {
				FacebookCache.ScoreImages.Add(score.id, pictureTexture);
				ProfilePicture.texture = pictureTexture;
			});
		}
		
	}

}