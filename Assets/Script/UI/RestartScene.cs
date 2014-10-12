using UnityEngine;
using System.Collections;

public class RestartScene : MonoBehaviour {

	void OnMouseDown()
	{
		StartCoroutine(load_game_after_delay());
	}
	
	IEnumerator load_game_after_delay()
	{
		yield return new WaitForSeconds(1);
		Application.LoadLevel(Application.loadedLevelName);
	}
}
