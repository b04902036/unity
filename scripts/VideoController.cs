using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoController : MonoBehaviour {
	public UnityEngine.Video.VideoPlayer Ad;
	public UnityEngine.Video.VideoPlayer Treebaby;
	public UnityEngine.Video.VideoPlayer F51;
	public UnityEngine.Video.VideoPlayer F52;
	public UnityEngine.Video.VideoPlayer F53;
	public UnityEngine.Video.VideoPlayer F54;
	public UnityEngine.Video.VideoPlayer F55;
	public UnityEngine.Video.VideoPlayer F56;
	public UnityEngine.Video.VideoPlayer F57;
	public UnityEngine.Video.VideoPlayer F58;
	public UnityEngine.Video.VideoPlayer TreeEnd;
	public GameObject Next;
	public GameObject Next2;
	public GameObject Next3;
	public GameObject Next4;
	public GameObject Replay;
	public GameObject End;

	private int Fivecounter = 1;
	private bool flag1, flag2;
	// Use this for initialization
	void Start () {
		Next.SetActive (false);
		Next2.SetActive (false);
		Next3.SetActive (false);
		Next4.SetActive (false);
		Replay.SetActive (false);
		End.SetActive (false);
		Ad.Play ();
		Treebaby.Prepare ();
		flag1 = false;
		flag2 = true;
	}
	public void penny(){
		F58.Stop ();
		TreeEnd.Play ();
		Next4.SetActive (false);
	}
	public void RestartVid(){
		Treebaby.Stop ();
		Treebaby.Play ();
		//Debug.Log ("Replay\n");
		flag1 = false;
		flag2 = true;
		Replay.SetActive (false);
		End.SetActive (false);
	}
	public void EndVid(){
		Treebaby.Stop ();
		TreeEnd.Stop ();
		//Debug.Log ("Stop\n");
		flag1 = false;
		flag2 = true;
		Replay.SetActive (false);
		End.SetActive (false);
		GameObject.Find ("WebcamTest").GetComponent<WebcamTest> ().contain = false;
		GameObject.Find ("WebcamTest").GetComponent<WebcamTest> ().Cansnap = true;
		Ad.Play ();
	}
	public void Skip(){
		flag1 = !flag1;
		flag2 = !flag2;
		TreeEnd.Prepare ();
		switch (Fivecounter) {
		case 1:
			F52.Play ();
			F51.Stop ();
			Fivecounter += 1;
			break;
		case 2:
			F53.Play ();
			F52.Stop ();
			Fivecounter += 1;
			break;
		case 3:
			F54.Play ();
			F53.Stop ();
			Fivecounter += 1;
			break;
		case 4:
			F55.Play ();
			F54.Stop ();
			Fivecounter += 1;
			break;
		case 5:
			F56.Play ();
			F55.Stop ();
			Fivecounter += 1;
			break;
		case 6:
			F57.Play ();
			F56.Stop ();
			Fivecounter += 1;
			break;
		case 7:
			F58.Play ();
			F57.Stop ();
			Fivecounter += 1;
			break;
		case 8:
			TreeEnd.Prepare ();
			F58.Stop ();
			Replay.SetActive (false);
			TreeEnd.Play ();
			Fivecounter = 1;
			break;
		default:
			print (Fivecounter);
			break;
		}
	}
	public void ResumeVid(){
		Next.SetActive (false);
		Next2.SetActive (false);
		Next3.SetActive (false);
		Next4.SetActive (false);
		if (Treebaby.time > 0) {
			Treebaby.Play ();
		} else if (TreeEnd.time > 0) {
			TreeEnd.Play ();
		}
		//Debug.Log ("Clicked\n");
		flag1 = !flag1;
		flag2 = !flag2;
	}
	// Update is called once per frame
	void Update () {
		//print (Treebaby.time);
		bool goo = GameObject.Find ("WebcamTest").GetComponent<WebcamTest> ().contain;
		if (goo == true && Treebaby.time == 0) {
			Ad.Stop ();
			Treebaby.Play ();
			End.SetActive (true);
			GameObject.Find ("WebcamTest").GetComponent<WebcamTest> ().Cansnap = false;
		}
		if (Treebaby.time > 0) {
			GameObject.Find ("WebcamTest").GetComponent<WebcamTest> ().Cansnap = false;
		}
		if (Treebaby.time >= 10) {
			End.SetActive (false);
		}
		if (Treebaby.time >= 16.3 && Treebaby.time < 22 && flag1 == false) {
			Treebaby.Pause ();
			F51.Prepare ();
			Next2.SetActive (true);
		}
		if (Treebaby.time >= 22.3 && Treebaby.time < 27.6 && flag2 == false) {
			Treebaby.Pause ();
			Next3.SetActive (true);
		}
		if(Treebaby.time >= 27.8 && flag1 == false){
			Treebaby.Pause ();
			Next.SetActive (true);
			F52.Prepare ();
			F53.Prepare ();
			F54.Prepare ();
			F55.Prepare ();
			F56.Prepare ();
			F57.Prepare ();
			F58.Prepare ();
		}
		if (Treebaby.time >= 27.8 && flag2 == false) {
			F51.Play ();
			GameObject.Find ("WebcamTest").GetComponent<WebcamTest> ().contain = false;
			Treebaby.Stop ();
		}
		if (F51.time >= 2.4) {
			Replay.SetActive (true);
		}
		if (F51.time >= 14.9) {
			F52.Play ();
			F51.Stop ();
		}
		if (F52.time >= 16.5) {
			F53.Play ();
			F52.Stop ();
			Fivecounter += 1;
		}
		if (F53.time >= 15) {
			F54.Play ();
			F53.Stop ();
			Fivecounter += 1;
		}
		if (F54.time >= 10.5) {
			F55.Play ();
			F54.Stop ();
			Fivecounter += 1;
		}
		if (F55.time >= 18.8) {
			F56.Play ();
			F55.Stop ();
			Fivecounter += 1;
		}if (F56.time >= 8.8) {
			F57.Play ();
			F56.Stop ();
			Fivecounter += 1;
		}
		if (F57.time >= 5.5) {
			F58.Play ();
			TreeEnd.Prepare();
			F57.Stop ();
			Fivecounter += 1;
		}
		if (F58.time >= 12) {
			Replay.SetActive (false);
			TreeEnd.Prepare ();
		}
		if (F58.time >= 23.5) {
			F58.Pause ();
			Next4.SetActive (true);
			Fivecounter = 1;
		}
		if (TreeEnd.time >= 5 && TreeEnd.time < 10 && flag2 == false) {
			TreeEnd.Pause ();
			Next.SetActive (true);
		}
		if (TreeEnd.time >= 11.3 && TreeEnd.time < 20 && flag1 == false) {
			TreeEnd.Pause ();
			Ad.Prepare ();
			Next.SetActive (true);
		}
		if (TreeEnd.time >= 16.5 && TreeEnd.time < 20 && flag2 == false) {
			EndVid ();
		}
	}
}
