using Facebook.Unity;
using SmartLocalization;
using UnityEngine;
using UnityEngine.UI;

public class FacebookManager : MonoBehaviour {

	public GameObject HeaderNotLoggedIn;
	public GameObject HeaderLoggedIn;
	public Button LoginButton;
	public RawImage ProfilePic;
	public Text WelcomeText;
	public Text ScoreText;

	private void Awake () {
		if (!FB.IsInitialized) FB.Init(InitCallback);
	}

	private void ChangeText (LanguageManager thislanguagemanager) {
		RedrawUI();
	}

	private void InitCallback () {
		AndroidJNIHelper.debug = false;
		FB.ActivateApp();

		if (FB.IsLoggedIn) Load();
	}

	private void Start () {
		if (FB.IsLoggedIn) RedrawUI();
		LanguageManager.Instance.OnChangeLanguage += ChangeText;
	}

	public void OnLoginClick () {
		// Disable the Login Button
		HeaderNotLoggedIn.SetActive(false);

		// Call Facebook Login for Read permissions of 'public_profile', 'user_friends', and 'email'
		FBLogin.PromptForLogin(LoginComplete);
	}

	private void LoginComplete () {
		if (!FB.IsLoggedIn) {
			// Reenable the Login Button
			HeaderNotLoggedIn.SetActive(true);
			return;
		}

		Load();
		FBLogin.PromptForPublish();
	}

	void Load () {
		FBGraph.GetPlayerInfo();
//		FBGraph.GetFriends();
//		FBGraph.GetInvitableFriends();
		FBGraph.GetScores();
		FBGraph.GetAchievements();
	}

	public void RedrawUI () {
		if (FB.IsLoggedIn) {
			// Swap GUI Header for a player after login
			HeaderLoggedIn.SetActive(true);
			HeaderNotLoggedIn.SetActive(false);

			// Show HighScore if we have one
			ScoreText.text = LanguageManager.Instance.GetTextValue("Facebook.Score") + FacebookInfo.HighScore.ToString();
		} else {
			HeaderNotLoggedIn.SetActive(true);
			return;
		}

		if (FacebookInfo.UserTexture != null && !string.IsNullOrEmpty(FacebookInfo.Username)) {
			// Update Profile Picture
			ProfilePic.enabled = true;
			ProfilePic.texture = FacebookInfo.UserTexture;

			// Update Welcome Text
			WelcomeText.text = LanguageManager.Instance.GetTextValue("Facebook.Welcome") + FacebookInfo.Username + "!";
		}
	}
	
	public void OnBragClicked()
	{
		Debug.Log("OnBragClicked");
		FBShare.ShareBrag();
	}

	public void OnChallengeClicked(string friendID)
	{
		Debug.Log("OnChallengeClicked");
		FBRequest.RequestChallenge(friendID);
	}

	private void OnDestroy () {
		if (LanguageManager.HasInstance) LanguageManager.Instance.OnChangeLanguage -= ChangeText;
	}

	public void TesteAchievement () {
		FBAchievements.GiveAchievement(Achievements.Teste);
	}

}