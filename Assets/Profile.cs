using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Profile : MonoBehaviour,IWaitingForInit {

	public Text nameLabel;
	[TextArea(1,10)]
	public string jsonDump;

	public void Init () 
	{
		//print ("Init Profile");
		API.instance.Call("/v1/me",OnReady);
	}
	
	private void OnReady(JSONObject j)
	{
		jsonDump = j.Print(true);
		nameLabel.text = j["me"]["username"].str;

	}


}
