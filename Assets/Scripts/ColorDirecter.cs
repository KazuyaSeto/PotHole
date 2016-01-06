using UnityEngine;
using System.Collections;
/// <summary>
/// Color directer.
/// ゲーム内の色を制御
/// メインカメラのSolidColor
/// フォグの背景色など
/// </summary>
public class ColorDirecter : MonoBehaviour {
	[SerializeField] Camera mainCamera = null;
	Color fogColor = Color.magenta;
	Color prevFogColor = Color.magenta;
	float fogChangeTimer = 0.0f;
	// Use this for initialization
	void Start () {
		mainCamera.backgroundColor = RenderSettings.fogColor;
	}
	
	// Update is called once per frame
	void Update () {
		RenderSettings.fogColor = Color.Lerp (prevFogColor, fogColor, fogChangeTimer);
		mainCamera.backgroundColor = RenderSettings.fogColor;
		if (fogChangeTimer < 1.0f) {
			fogChangeTimer += 0.01f;
		} else {
			fogChangeTimer = 1.0f; 
		}
	}

	/// <summary>
	/// Changes the color of the fog.
	/// フォグの色を変える
	/// </summary>
	public void ChangeFogColor() {
		fogChangeTimer = 0.0f;
		prevFogColor = fogColor;
		fogColor = new Color (	Random.Range(0.0f,1.0f),
								Random.Range(0.0f,1.0f),
								Random.Range(0.0f,1.0f));
	}
}
