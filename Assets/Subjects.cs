using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Subjects : MonoBehaviour,IWaitingForInit {

	public GameObject subject;
	[TextArea(1,10)]
	public string jsonDump;

	public void Init () 
	{
		//print ("Init Subjects");
		API.instance.Call("/v1/tablet",OnSubjectsReady);
	}
	
	private void OnSubjectsReady(JSONObject j)
	{
		jsonDump = j.Print(true);

		for (int i=0;i<j["subjects"].Count;i++)
		{
			GameObject go = Instantiate(subject);
			
			go.transform.SetParent(transform);
			go.GetComponent<Subject>().Init(j["subjects"][i]);

			
			
		}
		
	}

			                                       
                                           

}
