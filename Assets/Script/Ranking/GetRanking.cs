using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GetRanking : MonoBehaviour {

	private static GetRanking instance;

	void Awake() {
		instance = this;
	}

	public static void Get(Action<List<RankingUI.RankingData>> rankingCallback, Action failCallback) {
		instance.StartCoroutine(instance.Request (rankingCallback, failCallback));
	}

	IEnumerator Request(Action<List<RankingUI.RankingData>> rankingCallback, Action failCallback) {
		Debug.Log ("Send Ranking get request.");
		var request = new WWW ("http://cv.majecty.com:8080/app/chickentrio/rankings");
		yield return request;

		if (!String.IsNullOrEmpty (request.error)) {
			Debug.LogError("Ranking get www error: " + request.error);
			failCallback();
			yield break;
		}

		var jsonRankings = SimpleJSON.JSON.Parse (request.text);
		List<RankingUI.RankingData> rankingDatas = new List<RankingUI.RankingData> ();
		if (jsonRankings == null || jsonRankings.AsArray == null) {
			Debug.LogError("Ranking response is not valid format.");
			failCallback();
			yield break;
		}

		foreach (var rank in jsonRankings.AsArray.Childs) {
			string name = rank.AsObject["name"];
			int score = rank.AsObject["score"].AsInt;

			if (string.IsNullOrEmpty(name) && score == 0) {
				Debug.LogError("Cannot parse ranking data.");
				failCallback();
				yield break;
			}

			rankingDatas.Add(new RankingUI.RankingData(name, score));
		}

		rankingCallback(rankingDatas);
	}
}