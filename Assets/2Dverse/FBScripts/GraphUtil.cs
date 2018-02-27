/**
 * Copyright (c) 2014-present, Facebook, Inc. All rights reserved.
 *
 * You are hereby granted a non-exclusive, worldwide, royalty-free license to use,
 * copy, modify, and distribute this software in source code or binary form for use
 * in connection with the web services and APIs provided by Facebook.
 *
 * As with any software that integrates with the Facebook platform, your use of
 * this software is subject to the Facebook Developer Principles and Policies
 * [http://developers.facebook.com/policy/]. This copyright notice shall be
 * included in all copies or substantial portions of the software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
 * FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
 * COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
 * IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
 * CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using GoogleMobileAds.Api;
using UnityEngine;

// Utility class for useful operations when working with the Graph API
public class GraphUtil : ScriptableObject {

	// Generate Graph API query for a user/friend's profile picture
	public static string GetPictureQuery (string facebookID, int? width = null, int? height = null, string type = null,
		bool onlyURL = false) {
		string query = string.Format("/{0}/picture", facebookID);
		string param = width != null ? "&width=" + width.ToString() : "";
		param += height != null ? "&height=" + height.ToString() : "";
		param += type != null ? "&type=" + type : "";
		if (onlyURL) param += "&redirect=false";
		if (param != "") query += ("?g" + param);
		return query;
	}

	// Download an image using WWW from a given URL
	public static void LoadImgFromURL (string imgURL, Action<Texture> callback) {
		// Need to use a Coroutine for the WWW call, using Coroutiner convenience class
		Coroutiner.StartCoroutine(
			LoadImgEnumerator(imgURL, callback)
		);
	}

	public static IEnumerator LoadImgEnumerator (string imgURL, Action<Texture> callback) {
		WWW www = new WWW(imgURL);
		yield return www;

		if (www.error != null) {
//			Debug.LogError(www.error);
			yield break;
		}

		callback(www.texture);
	}

	// Pull out the picture image URL from a JSON user object constructed in FBGraph.GetPlayerInfo() or FBGraph.GetFriends()
	public static string DeserializePictureURL (object userObject) {
		// friendObject JSON format in this situation
		// {
		//   "first_name": "Chris",
		//   "id": "10152646005463795",
		//   "picture": {
		//      "data": {
		//          "url": "https..."
		//      }
		//   }
		// }
		var user = userObject as Dictionary<string, object>;

		object pictureObj;
		if (user.TryGetValue("picture", out pictureObj)) {
			var pictureData = (Dictionary<string, object>) (((Dictionary<string, object>) pictureObj)["data"]);
			return (string) pictureData["url"];
		}

		return null;
	}

	// Pull out score from a JSON user entry object constructed in FBGraph.GetScores()
	public static int GetScoreFromEntry (object obj) {
		Dictionary<string, object> entry = (Dictionary<string, object>) obj;
		return Convert.ToInt32(entry["score"]);
	}

	public static List<object> DeserializeAchievements (object userObject) {
		var user = userObject as Dictionary<string, object>;

		Dictionary<string, object> achievements;
		if (user.TryGetValue("achievements", out achievements)) {
			foreach (Dictionary<string, object> achievement in (List<object>) achievements["data"]) {
				achievement["id"] = ((Dictionary<string, object>)((Dictionary<string, object>)achievement["data"])["achievement"])["id"];

			}

			return (List<object>) achievements["data"];
		}

		return null;
	}
	
	public static void LoadImgFromID (string userID, Action<Texture> callback) {
		FB.API(GetPictureQuery(userID, 128, 128),
			HttpMethod.GET,
			delegate (IGraphResult result) {
				if (result.Error != null) {
					Debug.LogError(result.Error + ": for friend " + userID);
					return;
				}

				if (result.Texture == null) {
					Debug.Log("LoadFriendImg: No Texture returned");
					return;
				}

				callback(result.Texture);
			});
	}

	public static void TargetingAd () {
		if (!FacebookCache.UserGender.Equals(null) && !FacebookCache.UserBirthday.Equals(null)) return;
		FB.API("/me?fields=birthday,gender", HttpMethod.GET, callback);
	}

	private static void callback (IGraphResult result) {
		string gender;
		if (result.ResultDictionary.TryGetValue("gender", out gender)) {
			switch (gender) {
				case "male":
					FacebookCache.UserGender = Gender.Male;
					break;
				case "female":
					FacebookCache.UserGender = Gender.Female;
					break;
				default:
					FacebookCache.UserGender = Gender.Unknown;
					break;
			}
		}
		string birthday;
		if (result.ResultDictionary.TryGetValue("birthday", out birthday)) {
			FacebookCache.UserBirthday = DateTime.Parse(birthday);
		}
	}

}