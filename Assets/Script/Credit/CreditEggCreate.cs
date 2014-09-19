using UnityEngine;
using System.Collections;

public class CreditEggCreate : MonoBehaviour {
    public GameObject[] prefabs;
	// Use this for initialization

	void Start () {
	}

    void SendWaitCall()
    {
        StartCoroutine(CallAfterHalfSecond(0));
    }
    IEnumerator CallAfterHalfSecond(int count)
    {
        yield return new WaitForSeconds(0.5f);
        
        for (int i = 0; i < 6; i++)
        {
            int random_index = Random.Range(0, 3);
            GameObject random_prefab = prefabs[random_index];

            GameObject instance1 = Instantiate(random_prefab) as GameObject;
            instance1.transform.position = new Vector3(-2 + i, 2.7f, -3);
            iTween.MoveTo(instance1, new Vector3(-2 + i, -4.3f+count, -3), 0.5f);
        }
        if (count < 5)
            StartCoroutine(CallAfterHalfSecond(count + 1));
    }
	// Update is called once per frame
	void Update () {
	}
}
