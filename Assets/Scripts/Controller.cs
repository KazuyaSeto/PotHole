using UnityEngine;
using System.Collections;
/// <summary>
/// 入力管理クラス
/// 現在はキーボード入力のみ対応している
/// </summary>
public class Controller : MonoBehaviour {
	// ゲーム内で使用するボタン
	public enum Button {
		FIRE,
		UP,
		DOWN,
		LEFT,
		RIGHT,
	}

	/// <summary>
	/// ボタンが押され始めたかどうかを取得
	/// </summary>
	/// <returns><c>true</c>ボタンが押された <c>false</c>押されてない</returns>
	/// <param name="button">ボタン</param>
	public static bool GetButtonDown(Button button) {
		return Input.GetKeyDown (getKeyCode(button));
	}

	/// <summary>
	/// ボタンが押されているいるかどうかを取得
	/// </summary>
	/// <returns><c>true</c>ボタンが押されている<c>false</c>押されていない</returns>
	/// <param name="button">ボタン</param>
	public static bool GetButton(Button button) {
		return Input.GetKey (getKeyCode(button));
	}

	/// <summary>
	/// ボタンが離されたかどうかを取得
	/// </summary>
	/// <returns><c>true</c>ボタンが離された<c>false</c>離されていない、押されてもない</returns>
	/// <param name="button">Button.</param>
	public static bool GetButtonUp(Button button) {
		return Input.GetKeyUp (getKeyCode(button));
	}

	/// <summary>
	/// ボタンからキーコードを取得
	/// </summary>
	/// <returns>キーコード</returns>
	/// <param name="button">ボタン</param>
	static KeyCode getKeyCode(Button button) {
		switch(button) {
		case Button.FIRE:
			return KeyCode.Space;
		case Button.UP:
			return KeyCode.UpArrow;
		case Button.DOWN:
			return KeyCode.DownArrow;
		case Button.LEFT:
			return KeyCode.LeftArrow;
		case Button.RIGHT:
			return KeyCode.RightArrow;
		default :
			// ありえない
			return KeyCode.None;
		}
	}
}
