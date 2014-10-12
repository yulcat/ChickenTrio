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
	public TextMesh textMesh;
    // Use this for initialization
    void Start()
    {
        scoreValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
		textMesh.text = scoreValue.ToString ();
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