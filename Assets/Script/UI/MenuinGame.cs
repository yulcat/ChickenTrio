using UnityEngine;
using System.Collections;

public class MenuinGame : MonoBehaviour {

    public string pastSceneName = "title";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        Application.LoadLevel(pastSceneName);
    }
}
