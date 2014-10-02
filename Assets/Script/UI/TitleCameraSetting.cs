using UnityEngine;
using System.Collections;

public class TitleCameraSetting : MonoBehaviour {

	// Use this for initialization
	void Start () {

        if (Camera.main.aspect < 0.6)
        {
            camera.orthographicSize = 6.4f;
            //Debug.Log("16:9"); //0.56
        }
        else if (Camera.main.aspect < 0.7)
        {
            Camera.main.orthographicSize = 5.7f; 
            //Debug.Log("3:2"); //0.67
        }
        else
        {
            Camera.main.orthographicSize = 5.3f; 
            //Debug.Log("4:3");  //0.75
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
