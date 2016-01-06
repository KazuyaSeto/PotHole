using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Audio manager.
/// ゲーム内のoneshotを管理
/// oneshot名をenumで定義してenumを指定して再生する（大量に使用する予定がないため）
/// enumからoneshotを再生する
/// </summary>
public class AudioManager : MonoBehaviour {
	/// ワンショットseの名前を列挙型で定義
	public enum OneShotName {
		NONE, 
		CLEAR,
		BOOST,
		DAMAGE,
		HIGH_DAMAGE,
		CRASH,
		DEATH,
		THROUGH,
		GOAL
	}

	/// oneshotクラス
	/// unityのインスペクター上から設定する。
	[Serializable]
	public class OneShot {
		public OneShotName name = OneShotName.NONE;
		public AudioClip clip = null;
	}

	[SerializeField] AudioSource oneshotAudioSource = null;
	[SerializeField] List<OneShot> oneshotList = new List<OneShot>();

	Dictionary<OneShotName, AudioClip> oneshotDict = new Dictionary<OneShotName, AudioClip>();
	// Use this for initialization
	void Start () {
		foreach (var oneshot in oneshotList) {
			oneshotDict.Add (oneshot.name, oneshot.clip);
		}
	}

	/// <summary>
	/// ワンショットを再生する（se再生）
	/// </summary>
	/// <param name="oneshotName">ワンショット名</param>
	public void PlayOneShot(OneShotName oneshotName) {
		if (oneshotDict.ContainsKey (oneshotName)) {
			oneshotAudioSource.PlayOneShot (oneshotDict [oneshotName], 0.5f);
		}
	}
}
