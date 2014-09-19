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
            StartCoroutine(load_game_after_delay());
        }
		
	}

    IEnumerator load_game_after_delay()
    {
        iTween.CameraFadeTo(0.9f, 2);
        yield return new WaitForSeconds(1);
        Application.LoadLevel("title");
    }
}
