using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class WebcamTest : MonoBehaviour {
	public string deviceName;
	WebCamTexture wct;
	public bool contain = false;
	public bool Cansnap = true;

	// Use this for initialization
	void Start () {
		WebCamDevice[] devices = WebCamTexture.devices;
		deviceName = devices[0].name;
		for (int n = 0; n < devices.Length; ++n) {
			if (devices[n].isFrontFacing) {
				wct = new WebCamTexture(devices[n].name, 400, 300, 12);
				break;
			}
			if (n == devices.Length - 1) {
				wct = new WebCamTexture(deviceName, 400, 300, 12);
			}
		}
		wct.Play();
	}

	// For photo varibles

	//public Texture2D heightmap;
	public Vector3 size = new Vector3(100, 10, 100);

	void Update(){
		if (Cansnap == true) {
			Cansnap = false;
			TakeSnapshot ();
		}
	}
	/*
	void OnGUI() {      
		if (GUI.Button(new Rect(10, 70, 50, 30), "Click"))
			TakeSnapshot();
	}
*/


	void TakeSnapshot()
	{
		//print("snapped");
		Texture2D snap = new Texture2D(wct.width, wct.height);
		snap.SetPixels(wct.GetPixels());
		snap.Apply();

		StartCoroutine(Upload(snap));
	}

	IEnumerator Upload(Texture2D snap) {
		WWWForm form = new WWWForm ();
		byte [] bt = snap.EncodeToJPG();
		form.AddBinaryData ( "file", bt, "Jason","image/jpg");
		//print (bt.Length);
		form.AddField("name", "Jason");
		UnityWebRequest www = UnityWebRequest.Post ("http://192.168.173.1:8000/app1/upload", form);
		yield return www.SendWebRequest();
		while (!www.isDone) {
			//print ("idling");
		}
		UnityWebRequest www2 = UnityWebRequest.Get ("http://192.168.173.1:8000/app1/predict");
		yield return www2.SendWebRequest();
		string s = www2.downloadHandler.text;
		string search = "<h1>";
		int pos = s.IndexOf (search) + search.Length;
		int end = -1;
		if (pos < s.Length && pos >= 0) {
			end = s.IndexOf ("</h1>", pos);
		}
		//print (end);
		if (end >= 0) {
			string v = s.Substring (pos, end - pos);
			contain = (v == "1");
			if (contain == false) {
				Cansnap = true;
			}
		}
		//print (www2.downloadHandler.text);
		if(www.isNetworkError || www.isHttpError) {
			Debug.Log(www.error);
		}
		else {
			//Debug.Log("Form upload complete!");
		}
	}
}
