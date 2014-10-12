using UnityEngine;
using System.Collections;

public class ApplicationQuitAtClick : MonoBehaviour {
	void OnMouseDown()
	{
		StartCoroutine(load_game_after_delay());
	}
	
	IEnumerator load_game_after_delay()
	{
		yield return new WaitForSeconds(1);
		Application.Quit ();
	}
}
