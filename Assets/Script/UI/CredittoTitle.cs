using UnityEngine;
using System.Collections;

public class CredittoTitle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0))
        {
			Debug.Log ("In credit to title update mouse down.");
            StartCoroutine(load_game_after_delay());
        }
		
	}

    IEnumerator load_game_after_delay()
    {
		Debug.Log ("Move to title start.");
        iTween.CameraFadeTo(0.9f, 2);
        yield return new WaitForSeconds(1);
        Application.LoadLevel("title");
    }
}
