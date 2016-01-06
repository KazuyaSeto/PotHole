using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// タイトル画面UI制御クラス
/// </summary>
public class TitleUI : MonoBehaviour {
	[SerializeField] Image acceleratorImage = null; // 操作説明用のImage
	[SerializeField] Image controlerImage = null; // 操作説明用のImage

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Controller.GetButtonUp(Controller.Button.FIRE)) {
			GameManager.instance.changeScene (GameDef.SceneId.GAME);
		}

		UpdateDisplayDescription ();
	}

	/// <summary>
	/// 説明UIを更新
	/// マウスを画面下部に持っていくと表示される
	/// </summary>
	void UpdateDisplayDescription () {
		// 画面下部にマウスの移動すると操作説明が表示される
		float a = (-(Input.mousePosition.y + Screen.height * 0.7f) + Screen.height) * 3.0f / (float)Screen.height;
		Color color = new Color (1.0f, 1.0f, 1.0f, a);
		acceleratorImage.color = color;
		controlerImage.color = color;
	}
}
