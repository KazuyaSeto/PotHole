using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

/// <summary>
/// フェードインアウトの制御クラス
/// </summary>
public class SceneFadeInOut : MonoBehaviour
{
	[SerializeField] Image fadeImage = null;
	public float fadeSpeed = 1.0f;
	bool isFadeOut = false;
	bool isFadeIn = false;
	Action endSceneAction = null;
	void Awake () {
		DontDestroyOnLoad (fadeImage.transform.parent.gameObject);
	}
		
	void Update () {
		if (isFadeOut) {
			FadeOutProcess ();
		}
		if(isFadeIn) {
			FadeInProcess ();
		}
	}

	void FadeToClear () {
		// Lerp the colour of the texture between itself and transparent.
		fadeImage.color = Color.Lerp(fadeImage.color, Color.clear, fadeSpeed * Time.deltaTime);
	}

	void FadeToBlack () {
		// Lerp the colour of the texture between itself and black.
		fadeImage.color = Color.Lerp(fadeImage.color, Color.black, fadeSpeed * Time.deltaTime);
	}

	/// <summary>
	/// シーンスタート時のフェードアウト
	/// </summary>
	public void FadeOut() {
		isFadeOut = true;
		isFadeIn = false;
	}
		
	void FadeOutProcess () {
		// Fade the texture to clear.
		FadeToClear();

		// If the texture is almost clear...
		if(fadeImage.color.a <= 0.05f)
		{
			// ... set the colour to clear and disable the GUITexture.
			fadeImage.color = Color.clear;
			fadeImage.enabled = false;

			// The scene is no longer starting.
			isFadeOut = false;
		}
	}

	/// <summary>
	/// シーン終了時のフェードイン
	/// </summary>
	/// <param name="action">Action.</param>
	public void FadeIn (Action action) {
		endSceneAction = action;
		isFadeIn = true;
		isFadeOut = false;
	}

	void FadeInProcess ()
	{
		// Make sure the texture is enabled.
		fadeImage.enabled = true;

		// Start fading towards black.
		FadeToBlack();

		// If the screen is almost black...
		if (fadeImage.color.a >= 0.95f) {
			// ... reload the level.
			isFadeIn = false;
			endSceneAction ();
			endSceneAction = null;
		}
	}
}