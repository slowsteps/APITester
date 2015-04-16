using UnityEngine;
using System.Collections;

public class Avatar : MonoBehaviour,IWaitingForInit {

	private Vector3 spinAxis;

	
	public void Init () 
	{
		print ("Init Avatar");
		API.instance.Call("/v1/me",OnReady);
		
	}

	void Start () 
	{
		spinAxis = Random.insideUnitSphere;
	}
	


	private void OnReady(JSONObject j)
	{
		print ("display name: " + j["me"]["display_name"].str);
		API.instance.GetImage(j["me"]["avatar_url"].str,OnImageReady);
	}

	private void OnImageReady(Texture texture)
	{
		gameObject.GetComponent<Renderer>().material.mainTexture = texture;
	}
	

	// Update is called once per frame
	void Update () 
	{
		transform.Rotate(spinAxis,1);
	}
}
