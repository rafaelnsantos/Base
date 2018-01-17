using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LeaderBoardElement : MonoBehaviour
{
	//   Element references (Set in Unity Editor)   //
//	public Image Background;
	public RawImage ProfilePicture;
	public Text Rank;
	public Text Name;
	public Text Score;

	public void SetupElement (int rank, object entryObj)
	{
		var entry = (Dictionary<string,object>) entryObj;
		var user = (Dictionary<string,object>) entry["user"];

		Rank.text = rank.ToString() + ".";
		Name.text = ((string) user["name"]).Split(new char[]{' '})[0];
		Score.text = "Smashed: " + GraphUtil.GetScoreFromEntry(entry).ToString();

		Texture picture;
		if (FacebookInfo.FriendImages.TryGetValue((string)user["id"], out picture)) 
		{
			ProfilePicture.texture = picture;
		}
	}
}
