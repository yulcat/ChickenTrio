using UnityEngine;
using System.Collections;

partial class Score : MonoBehaviour
{
    private static Score instance = null;

    public static Score Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(Score)) as Score;
            return instance;
        }
    }
}

partial class Score : MonoBehaviour
{
    private int scoreValue;
}

partial class Score : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        scoreValue = 0;
        if (Camera.main.aspect < 0.6)
        {
            guiText.fontSize = 20;
            guiText.transform.position = new Vector3(0.5f, 0.87f, 0f);
            Debug.Log("16:9"); //0.56
        }
        else if (Camera.main.aspect < 0.7)
        {
            guiText.fontSize = 22;
            guiText.transform.position = new Vector3(0.52f, 0.94f, 0f);
            Debug.Log("3:2"); //0.67
        }
        else
        {
            guiText.fontSize = 23;
            guiText.transform.position = new Vector3(0.52f, 0.94f, 0f);
            Debug.Log("4:3");  //0.75
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnGUI()
    {
        guiText.text = scoreValue.ToString();
    }
}

partial class Score : MonoBehaviour
{
    public void setScore(int newScore)
    {
        scoreValue = newScore;
    }

    public int getScore()
    {
        return scoreValue;
    }
}