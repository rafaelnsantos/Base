using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using SmartLocalization;

public class AchievementElement : MonoBehaviour {

	//   Element references (Set in Unity Editor)   //
//	public Image Background;
	public RawImage AchievementPicture;

	public Text Title;
	public Text Description;
	public Text CompleteDate;
	public bool Completed { get; private set; }

	public void SetupElement (object entryObj) {
		var entry = (Dictionary<string, object>) entryObj;

		Title.text = (string) entry["title"];
		Description.text = (string) entry["description"];

		Texture picture;
		if (FacebookInfo.AchievementsImages.TryGetValue((string) entry["id"], out picture)) {
			AchievementPicture.texture = picture;
		}
		CompleteDate.text = "";

		if (FacebookInfo.CompletedAchievements == null) return;

		foreach (Dictionary<string, object> completedAchievement in FacebookInfo.CompletedAchievements) {
			if (!completedAchievement["id"].Equals(entry["id"])) continue;

			var data = DateTime.Parse(completedAchievement["end_time"].ToString());
			CompleteDate.text = LanguageManager.Instance.GetTextValue("Facebook.DateAchievement") + data.ToLocalTime().ToString("dd MMM, yyyy '@' HH:mm");
			AchievementPicture.color = Color.white;
			Completed = true;
		}
	}

}