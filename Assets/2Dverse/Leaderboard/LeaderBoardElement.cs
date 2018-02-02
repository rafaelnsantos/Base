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

		ProfilePicture.texture = score.user.picture;
		Name.text = score.user.name;

	}

}