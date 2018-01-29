using Facebook.Unity;
using GraphQL;
using UnityEngine;

public class FacebookManager : MonoBehaviour {

	public GameObject HeaderNotLoggedIn;
	public GameObject HeaderLoggedIn;

	private void Awake () {
		if (!FB.IsInitialized) {
			FB.Init(InitCallback);
		} else {
			if (!FB.IsLoggedIn) {
				HeaderNotLoggedIn.SetActive(true);
			} else {
				HeaderLoggedIn.SetActive(true);
				APIGraphQL.Token = AccessToken.CurrentAccessToken.TokenString;
			}
		}
	}

	private void InitCallback () {
		AndroidJNIHelper.debug = false;
		FB.ActivateApp();

		if (FB.IsLoggedIn) {
			APIGraphQL.Token = AccessToken.CurrentAccessToken.TokenString;
			HeaderLoggedIn.SetActive(true);
		} else {
			HeaderNotLoggedIn.SetActive(true);
		}
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
		APIGraphQL.Token = AccessToken.CurrentAccessToken.TokenString;
		HeaderLoggedIn.SetActive(true);
	}

	public void OnBragClicked () {
//		Debug.Log("OnBragClicked");
		FBShare.ShareBrag();
	}

	public void OnChallengeClicked (string friendID) {
//		Debug.Log("OnChallengeClicked");
		FBRequest.RequestChallenge(friendID);
	}

	public void TesteAchievement () {
		FBAchievements.GiveAchievement(Achievements.Teste);
	}

}