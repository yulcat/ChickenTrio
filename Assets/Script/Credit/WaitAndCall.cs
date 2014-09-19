using UnityEngine;
using System.Collections;

public class WaitAndCall  : MonoBehaviour {
    public float second;
	// Use this for initialization
	void Start () {
        StartCoroutine(WaitSec());

	}
    IEnumerator WaitSec()
    {
        yield return new WaitForSeconds (second);
        gameObject.SendMessage("SendWaitCall");
    }

	// Update is called once per frame
	void Update () {
	
	}
}
