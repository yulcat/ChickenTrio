using UnityEngine;
using System.Collections;

public class NextRollEgg : MonoBehaviour {
    bool flag;
	// Use this for initialization
	void Start () {
        flag = true;
		}
	
	// Update is called once per frame
	void Update () {
     if(flag==false){
            transform.RotateAround(new Vector3(0, 0, 1), 6);
            transform.position += new Vector3(0.1f, 0, 0);
     }
	
	}
    void OnTriggerEnter(Collider collision)
    {
        flag = false;

    }

 
}
