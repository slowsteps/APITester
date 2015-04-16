using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IWaitingForInit
{
	void Init();
}

public class API : MonoBehaviour {

	public GameObject cube;
	public Texture2D avatarTexture;
	public static API instance;
	public string baseURL = "http://api2.squla.nl";
	[TextArea(1,10)]
	public string accessToken = "";
	[TextArea(1,10)]
	public string refreshToken = "eyJjIjo1LCJyIjoxNTY0MjksInUiOjM4MDYxNn0.CA_mYw.jsQDv2GxScHBry2dnv9LYw7-cMM";
	public GameObject[] waitingForInitList;
	public delegate void callBack(JSONObject j);
	public delegate void imageCallBack(Texture2D texture);
	
	void Awake () 
	{
		instance = this;
		accessToken = PlayerPrefs.GetString ("access_token");
		InvokeRepeating("KeepTokenFresh",0f,60f);
	}
	
	public void Call(string endpoint,callBack callback)
	{
		StartCoroutine(GetURL(endpoint,callback));	
	}

	IEnumerator GetURL (string endpoint,callBack callback)
	{
		//print("in GetURL");
		string url = baseURL + endpoint + "?access_token="+accessToken;
		WWW request = new WWW(url);
		yield return request;
		JSONObject j = new JSONObject(request.text);
		yield return j;
		callback(j);
	}

	private void KeepTokenFresh()
	{
		StartCoroutine(RefreshToken());
	}

	IEnumerator RefreshToken()
	{
		//get a new accessToken via the refresh token
		

		WWWForm form = new WWWForm ();
		form.AddField ("ping", "pong"); // add form to force post method
		string url = "https://leukleren.squla.nl/oauth2/token?client_id=androidapp@squla.live&client_secret=WlrOUFhyYpGXXsi1INvx1iJt9aY&refresh_token=WzUsMzgwNjE2LG51bGxd.CA7IWA.ogiIWAxOkGaN9rdna8WfmMTTlAM&grant_type=refresh_token";

		WWW www = new WWW(url,form);
		yield return www;

		JSONObject j = new JSONObject(www.text);
		print ("new access_token " + j["access_token"].str);
		accessToken = j ["access_token"].str;
		PlayerPrefs.SetString ("access_token", accessToken);
	
		//Init the objects when authorisation is possible
		InitDoesWhoAreWaiting();

	}

	private void InitDoesWhoAreWaiting()
	{
		foreach (GameObject child in waitingForInitList) 
		{
			child.GetComponent<IWaitingForInit>().Init();	
		}
	}

	public Texture2D GetImage(string url,imageCallBack imagecallback)
	{
		StartCoroutine(GetImageFromWWW(url,imagecallback));
		return null;
	}

	IEnumerator GetImageFromWWW(string url,imageCallBack imagecallback)
	{
		//print ("GetImageFromWWW");
		WWW request = new WWW (url);
		yield return request;
		imagecallback(request.texture);
		
	}


}
