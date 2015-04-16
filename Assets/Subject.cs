using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Subject : MonoBehaviour {


	[TextArea(1,10)]
	public string jsonDump;
	
	public void Init(JSONObject j)
	{
		jsonDump =  j.Print(true);
		string subjectname = j["title"].str;
		gameObject.GetComponent<Text>().text = subjectname;
		gameObject.name = subjectname;
		API.instance.GetImage(j["icon_url"].str,OnSubjectImageLoaded);
	
		
	}


	public void OnSubjectImageLoaded(Texture2D texture)
	{
		gameObject.GetComponentInChildren<Image>().sprite = Sprite.Create(texture,new Rect(0,0,texture.width,texture.height),Vector2.zero);	
	}		

}
