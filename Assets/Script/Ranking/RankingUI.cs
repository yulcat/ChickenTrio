using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RankingUI : MonoBehaviour {
	private static RankingUI instance;
	public static RankingUI Get() {
		return instance;
	}
	
	void Awake() {
		instance = this;
	}

	void Start() {
		GetRanking.Get (SetRanking, () => {});
	}

	public List<GameObject> rankingElements;

	public class RankingData {
		public readonly string name;
		public readonly int score;

		public RankingData (string name, int score)
		{
			this.name = name;
			this.score = score;
		}
	}

	public void SetRanking(List<RankingData> rankingDatas) {
		Queue<RankingData> rankingDataQueue = new Queue<RankingData>(rankingDatas);

		foreach (GameObject rankingElement in rankingElements) {
			if (rankingDataQueue.Count < 1) {
				break;
			}

			TextMesh nameText = rankingElement.transform.Find("name").GetComponent<TextMesh>();
			TextMesh scoreText = rankingElement.transform.Find("score").GetComponent<TextMesh>();

			var rankingData = rankingDataQueue.Dequeue();
			nameText.text = rankingData.name;
			scoreText.text = rankingData.score.ToString();
		}
	}
}