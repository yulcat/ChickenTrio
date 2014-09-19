using UnityEngine;
using System.Collections;

public class StartinTitle : MonoBehaviour {

    public string nextSceneName = "game";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnMouseDown()
    {
        StartCoroutine(load_game_after_delay());
    }

    IEnumerator load_game_after_delay()
    {
        iTween.CameraFadeTo(0.9f, 2);
        yield return new WaitForSeconds(1);
        Application.LoadLevel(nextSceneName);
    }
}
