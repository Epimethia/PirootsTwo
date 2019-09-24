using UnityEngine;
using System.Collections;
using TwitterKit.Unity;
using UnityEngine.UI;

public class TwitterDemo : MonoBehaviour
{
	public Image iconImage;
	public void startLogin() {
		UnityEngine.Debug.Log ("startLogin()");
		// To set API key navigate to tools->Twitter Kit
		Twitter.Init ();

		Twitter.LogIn (LoginCompleteWithCompose, (ApiError error) => {
			UnityEngine.Debug.Log (error.message);
		});
	}
	
	public void LoginCompleteWithEmail (TwitterSession session) {
		// To get the user's email address you must have "Request email addresses from users" enabled on https://apps.twitter.com/ (Permissions -> Additional Permissions)
		UnityEngine.Debug.Log ("LoginCompleteWithEmail()");
		Twitter.RequestEmail (session, RequestEmailComplete, (ApiError error) => { UnityEngine.Debug.Log (error.message); });
	}
	
	public void RequestEmailComplete (string email) {
		UnityEngine.Debug.Log ("email=" + email);
		LoginCompleteWithCompose ( Twitter.Session );
	}
	
	public void LoginCompleteWithCompose(TwitterSession session) {
		string imageUri = "file:///" + Application.persistentDataPath + "/Piroots.png";
		
		Twitter.Compose (session, imageUri, "I caught a massive fish! Come play :D ", new string[]{"#PirootsoftheCurrybean"},
			(string tweetId) => { UnityEngine.Debug.Log ("Tweet Success, tweetId=" + tweetId); },
			(ApiError error) => { UnityEngine.Debug.Log ("Tweet Failed " + error.message); },
			() => { Debug.Log ("Compose cancelled"); }
		 );

	}
}
