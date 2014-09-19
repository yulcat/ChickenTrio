using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {

	// Use this for initialization
	void Start () {
        iTween.CameraFadeAdd();
        iTween.CameraFadeFrom(0.9f, 1f);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
