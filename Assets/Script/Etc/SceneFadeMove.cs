using System.Collections;
using UnityEngine;

public class SceneFadeMove : MonoBehaviour
{
    public string nextSceneName = "title";
    // Use this for initialization
    void Start()
    {
        StartCoroutine(load_menu_after_delay());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator load_menu_after_delay()
    {
        //yield return new WaitForSeconds(0.5f);
        iTween.CameraFadeAdd();
        iTween.CameraFadeFrom(0.9f, 1f);
        yield return new WaitForSeconds(2.0f);
        iTween.CameraFadeTo(0.9f, 2);
        yield return new WaitForSeconds(1);
        Application.LoadLevel(nextSceneName);
    }
}