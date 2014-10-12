using UnityEngine;
using System.Collections;

public class MenuinGame : MonoBehaviour {

	public GameObject asktoTitle;
	public GameObject asktoQuit;
	private bool isBoxThere = false;
	private GameObject askbox;

	// Use this for initialization
	void Start () {
	
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)&&!isBoxThere) 
		{
			askbox = Instantiate(asktoQuit) as GameObject;
			isBoxThere=true;
		}
		else if(Input.GetKeyDown(KeyCode.Escape)&&isBoxThere){
			Destroy (askbox);
			isBoxThere=false;
		}
		
	}
	
	// Update is called once per frame
    void OnMouseDown()
    {
		if(!isBoxThere){
			isBoxThere=true;
			askbox = Instantiate(asktoTitle) as GameObject;
		}
		else if(isBoxThere){
			Destroy (askbox);
			isBoxThere=false;
		}
    }


}
