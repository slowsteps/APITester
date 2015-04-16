using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Subjects : MonoBehaviour,IWaitingForInit {

	public GameObject subject;
	[TextArea(1,10)]
	public string jsonDump;

	public void Init () 
	{
		print ("Init Subjects");
		API.instance.Call("/v1/tablet",OnSubjectsReady);
	}
	
	private void OnSubjectsReady(JSONObject j)
	{
		jsonDump = j.str;

		for (int i=0;i<j["subjects"].Count;i++)
		{
			string subjectname = j["subjects"][i]["title"].str;
			GameObject go = Instantiate(subject);
			go.GetComponent<Text>().text = subjectname;
			go.transform.parent = this.transform;
			go.name = subjectname;
		}
		
	}
}
