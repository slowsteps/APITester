using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Profile : MonoBehaviour,IWaitingForInit {

	public Text nameLabel;

	public void Init () 
	{
		print ("Init Profile");
		API.instance.Call("/v1/me",OnReady);
	}
	
	private void OnReady(JSONObject j)
	{
		//nameLabel.text = j["me"]["username"].str;
	}


}
