﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RankingUIInGame : MonoBehaviour {

	static RankingUIInGame instance;
	public static RankingUIInGame Get() {
		return instance;
	}

	void Awake() {
		instance = this;
		isSended = false;
	}

	public GameObject newRecord;
	bool isReceived = false;
	int score = 0;
	string name = string.Empty;

	bool isSended = false;

	public void OnGameEnd(int score) {
		this.score = score;
		isReceived = false;
		if (isSended == false) {
			isSended = true;
			GetRanking.Get (OnReceiveRanking, () => {});
		} else {
			Debug.LogWarning("Ranking save request duplicated.");
		}
	}

	TouchScreenKeyboard keyboard;
	void ShowNameEnter() {
		newRecord.SetActive (true);
		keyboard = TouchScreenKeyboard.Open ("");
	}

	void Update() {
        if (keyboard != null) {
            newRecord.GetComponentInChildren<TextMesh>().text = keyboard.text;
        }
		if (keyboard != null && keyboard.done) {
			name = new string(keyboard.text.Take(10).ToArray());;
			if (!string.IsNullOrEmpty(name)) {
				StartCoroutine(SetScore(name, score));
			} else {
				Debug.LogError("Name is not valid.");
			}
			keyboard = null;
		}
	}

	IEnumerator SetScore(string name, int score) {
		var form = new WWWForm ();
		form.AddField ("name", name);
		form.AddField ("score", score);

		var www = new WWW ("http://cv.majecty.com:8080/app/chickentrio/rankings", form);
		yield return www;

		if (!string.IsNullOrEmpty (www.error)) {
			Debug.LogError("Error when save rankings.");
			Debug.LogError(www.error);
			yield break;
		}

		Application.LoadLevel ("ranking");
	}

	public void OnReceiveRanking(List<RankingUI.RankingData> rankingDatas) {
		isReceived = true;

		if (rankingDatas.Count < 10) {
			ShowNameEnter();
		} else {
			int minScore = int.MaxValue;
			foreach (var rankingData in rankingDatas) {
				if (rankingData.score < minScore) {
					minScore = rankingData.score;
				}
			}

			if (minScore > score) {
				ShowNameEnter();
			}
		}
	}
}
