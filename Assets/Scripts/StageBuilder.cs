using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// スタージを無限に作成するクラス
/// 外壁と障害物を生成する
/// </summary>
public class StageBuilder : MonoBehaviour {
	[SerializeField] Player player = null;
	[SerializeField] TonnelLoop loopcip = null; // 外壁のオブジェクト」
	[SerializeField] List<StageCip> stagecips = new List<StageCip>();

	List<TonnelLoop> loopcips = new List<TonnelLoop>();
	int loopcipcount = 0;
	// Use this for initialization
	void Start () {
		// ステージ初期化
		for(int index = 0; index < GameDef.TONNEL_LOOP_SIZE; index++) {
			TonnelLoop loop = Instantiate<TonnelLoop> (loopcip);
			loop.transform.parent = transform;
			loopcips.Add ( Instantiate<TonnelLoop>(loopcip) );
		}

		// 外壁の初期位置を設定
		for(int index = 0; index < GameDef.TONNEL_LOOP_SIZE; index++) {
			Vector3 position = loopcips [GameDef.TONNEL_LOOP_SIZE - 1 - index].transform.position + Vector3.up * ((1 - index) * loopcip.depth);
			loopcips [GameDef.TONNEL_LOOP_SIZE - 1 - index].transform.position = position;
		}

		// 障害物の生成を開始
		StartCoroutine (createStage());
	}
	
	// Update is called once per frame
	void Update () {
		if (player.transform.position.y < -loopcip.depth * loopcipcount) {
			TonnelLoop tail = loopcips [GameDef.TONNEL_LOOP_SIZE - 1];
			loopcips.RemoveAt (GameDef.TONNEL_LOOP_SIZE -1);
			tail.transform.position = tail.transform.position + (Vector3.down * (loopcip.depth * GameDef.TONNEL_LOOP_SIZE)) ;
			loopcipcount++;
			loopcips.Insert (0,tail);
		}
			
	}

	/// <summary>
	/// 障害物の生成コルーチン
	/// </summary>
	/// <returns>コルーチン</returns>
	IEnumerator createStage() {
		float createPointDepth = GameDef.STAGE_START_DEPTH;
		while (true) {
			// マップチップを選択
			var nextStageCip = getNextStageCip ();
			int repeatCount = nextStageCip.repeatCount;
			// リピート回数を試行する。
			for (int count = 0; count < repeatCount; count++) {
				createStageCip (nextStageCip, createPointDepth, count);
				createPointDepth += -nextStageCip.depth;

				yield return StartCoroutine(WaitForPlayerFalling(createPointDepth));
				if(createPointDepth < GameDef.GOAL_DEPTH) {
					yield break;
				}
			}

			// 障害物生成後の空白の生成
			createPointDepth += -nextStageCip.coolDepth;

			yield return StartCoroutine(WaitForPlayerFalling(createPointDepth));
			if(createPointDepth < GameDef.GOAL_DEPTH) {
				yield break;
			}
		}
	}

	/// <summary>
	/// 次に生成する障害物を取得する
	/// </summary>
	/// <returns>選択された障害物</returns>
	StageCip getNextStageCip () {
		// ランダムで選択する
		int stageCipIndex = Random.Range (0, stagecips.Count);
		StageCip nextStageCip = stagecips [stageCipIndex];
		return nextStageCip;
	}

	/// <summary>
	/// 障害物の生成処理
	/// </summary>
	/// <param name="stageCip">生成する障害物</param>
	/// <param name="creatPointDepth">生成する深さ</param>
	/// <param name="count">生成中の障害物のカウント</param>
	void createStageCip(StageCip stageCip, float creatPointDepth, int count) {
		StageCip cip = Instantiate<StageCip> (stageCip);
		cip.transform.position = Vector3.up * creatPointDepth;

		switch(cip.generatedPattern) {
		case GameDef.StageCipGeneratedPattern.LINER:
			// 線形に回転する
			cip.transform.Rotate (Vector3.up, cip.rotateValue * count + cip.initialValue);
			break;
		case GameDef.StageCipGeneratedPattern.PING_PONG:
			// 半分まで生成すると回転が逆転する
			if (count < cip.repeatCount / 2) {
				cip.transform.Rotate (Vector3.up, cip.rotateValue * count + cip.initialValue);
			} else {
				cip.transform.Rotate (Vector3.up, cip.rotateValue * (cip.repeatCount - count) + cip.initialValue);
			}
			break;
		case GameDef.StageCipGeneratedPattern.RANDOM:
			// ランダムで生成する
			if (cip.rotateValue != 0.0f) {
				cip.transform.Rotate (Vector3.up, cip.rotateValue * Random.Range(0, (360.0f / cip.rotateValue)) + cip.initialValue);
			} else {
				cip.transform.Rotate (Vector3.up, cip.initialValue);
			}
			break;
		}
	}
		
	/// <summary>
	/// 障害物を生成する深さまでプレイヤーが落ちるのを待つ
	/// </summary>
	/// <returns>コルーチン</returns>
	/// <param name="createPointDepth">障害物を生成する深さ</param>
	IEnumerator WaitForPlayerFalling(float createPointDepth) {
		var distance = 200.0f;
		var checkWaitTime = 0.5f;
		while (UnityEngine.Mathf.Abs (player.transform.position.y - createPointDepth) > distance) {
			yield return new WaitForSeconds (checkWaitTime);
		}
	}
}
