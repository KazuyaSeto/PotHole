using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// ゲームメイン画面UIの制御
/// ブーストスライダー、現在の深さ、現在の速度の制御
/// </summary>
public class MainUI : MonoBehaviour {
	[SerializeField] Player player = null; // プレイヤーの参照
	[SerializeField] ImageNumber depthImageNumber = null; // 現在の深さ表示スプライトナンバーUI
	[SerializeField] ImageNumber speedImageNumber = null; // スピードを表示するスプライトナンバーUI
	[SerializeField] Slider boostSliderRight = null;
	[SerializeField] Slider boostSliderLeft = null;

	Color sliderColorNormal = (Color.red - new Color(0.0f, 0.0f, 0.0f, 0.5f));
	Color sliderColorMax = (Color.cyan - new Color(0.0f, 0.0f, 0.0f, 0.5f));

	Rigidbody playerRigidbody = null;
	Image boostSliderRightImage = null;
	Image boostSliderLeftImage = null;

	// Use this for initialization
	void Start () {
		playerRigidbody = player.GetComponent<Rigidbody> ();
		boostSliderRightImage = boostSliderRight.fillRect.GetComponent<Image> ();
		boostSliderLeftImage = boostSliderLeft.fillRect.GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateDisplayNumber ();
		UpdateBoostSlider ();
	}

	/// <summary>
	/// 数値表示部分を更新
	/// </summary>
	void UpdateDisplayNumber () {
		depthImageNumber.updateNumber(-(int)player.transform.position.y);
		speedImageNumber.updateNumber(-(int)(playerRigidbody.velocity.y * 100));
	}

	/// <summary>
	/// ブーストスライダー部分を更新
	/// </summary>
	void UpdateBoostSlider () {
		if(player.boost > GameDef.MAX_BOOST_VALUE - 0.05f) {
			boostSliderRightImage.color = sliderColorMax;
			boostSliderLeftImage.color = sliderColorMax;
		} else {
			boostSliderRightImage.color = sliderColorNormal;
			boostSliderLeftImage.color = sliderColorNormal;
		}

		boostSliderRight.value = player.boost;
		boostSliderLeft.value = player.boost;
	}

	void OnDestroy() {
		playerRigidbody = null;
		boostSliderLeftImage = null;
		boostSliderRightImage = null;
	}
}
