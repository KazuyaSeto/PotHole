using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// スコア表示UI制御クラス
/// </summary>
public class ScoreUI : MonoBehaviour {
	[SerializeField] ImageNumber scoreImageNumber = null;

	// Use this for initialization
	void Start () {
		scoreImageNumber.updateNumber (-(int)GameManager.instance.score);
	}
	
	// Update is called once per frame
	void Update () {
		if(Controller.GetButtonUp(Controller.Button.FIRE)) {
			GameManager.instance.changeScene (GameDef.SceneId.GAME);
		}
	}
}
