using UnityEngine;
using System.Collections;

public class BackButtonClick : MonoBehaviour {
	
	public GameObject asktoTitle;
	private bool isBoxThere = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)&&!isBoxThere) 
		{
			GameObject askbox = Instantiate(asktoTitle) as GameObject;
			isBoxThere=true;
		}
		
	}
}
