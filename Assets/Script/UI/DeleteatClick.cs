using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections;

public class DeleteatClick : MonoBehaviour {

    public string pastSceneName = "title";

    int number;
    string numbername;

	// Use this for initialization
	void Start () {
        string name = gameObject.name;
        numbername = Regex.Replace(name, @"TutorialImage", "");
        number = int.Parse(numbername);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            number--;
            if (numbername == "7" && number <= 0) StartCoroutine(load_game_after_delay());
            else if (number <= 0 && numbername != "7") Destroy(gameObject);
        }
	}

    IEnumerator load_game_after_delay()
    {
        iTween.CameraFadeTo(0.9f, 2);
        yield return new WaitForSeconds(1);
        Application.LoadLevel("title");
    }
}
