using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using SmartLocalization;

public class AchievementElement : MonoBehaviour {

	//   Element references (Set in Unity Editor)   //
//	public Image Background;
	public RawImage AchievementPicture;

	public TextController Title;
	public TextController Description;
	public Text CompleteDate;
	public bool Completed { get; private set; }

	public void SetupElement (object entryObj) {
		var entry = (Dictionary<string, object>) entryObj;

		Title.SetKey((string) entry["title"]);
		Description.SetKey((string) entry["description"]);

		Texture picture;

		string id = (string) entry["id"];
		if (FacebookCache.AchievementsImages.TryGetValue(id, out picture)) {
			AchievementPicture.texture = picture;
		} else {
			// We don't have this achievement image yet, request it now
			GraphUtil.LoadImgFromID(id, pictureTexture => {
				if (pictureTexture != null) {
					FacebookCache.AchievementsImages.Add(id, pictureTexture);
					AchievementPicture.texture = pictureTexture;
				}
			});
		}
		CompleteDate.text = "";

		if (FacebookCache.CompletedAchievements == null) return;

		foreach (Dictionary<string, object> completedAchievement in FacebookCache.CompletedAchievements) {
			if (!completedAchievement["id"].Equals(entry["id"])) continue;

			var data = DateTime.Parse(completedAchievement["end_time"].ToString());
			CompleteDate.text = LanguageManager.Instance.GetTextValue("Facebook.DateAchievement") +
			                    data.ToLocalTime().ToString("dd MMM, yyyy '@' HH:mm");
			AchievementPicture.color = Color.white;
			Completed = true;
		}
	}

}