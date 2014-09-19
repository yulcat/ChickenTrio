using UnityEngine;
using System.Collections;

public class FirstRollEgg : MonoBehaviour {
    bool flag;
	// Use this for initialization
	void Start () {
        flag = false;
    }
	// Update is called once per frame
    void Update()
    {
        if (flag == true)
        {
            transform.RotateAround(new Vector3(0, 0, 1), 6);
            transform.position += new Vector3(0.1f, 0, 0);
        }
    }

    void SendWaitCall()
    {
        flag = true;
        Debug.Log("called");
    }
    void OnTriggerEnter(Collider collision)
    {
        flag = false;
   
    }

}
    