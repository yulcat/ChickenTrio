using UnityEngine;
using System.Collections;

public class AskToQuitExit : MonoBehaviour {
	void OnMouseDown() {
		GameObject.FindObjectOfType<MenuinGame> ().CloseAskToQuit ();
	}
}
