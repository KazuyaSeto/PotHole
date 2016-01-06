using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// スプライトで数字を表示する制御クラス
/// 桁のスプライト(index0から一桁目)と
/// 数字スプライト(index0から0,1,2...9を設定)の指定をインスペクター上から設定
/// 0以下はサポート外
/// </summary>
public class ImageNumber : MonoBehaviour {
	[SerializeField] List<Image> imageList = null;
	[SerializeField] List<Sprite> spriteList = null;

	/// <summary>
	/// 数字スプライトを更新
	/// </summary>
	/// <param name="value">表示する値</param>
	public void updateNumber(int value) {
		if (value < 0) {
			value = 0;
		}
		updateImageNumbers (value, imageList, spriteList);
	}

	/// <summary>
	/// 数字スプライトの更新処理
	/// </summary>
	/// <param name="value">表示する値</param>
	/// <param name="imageNumbers">桁スプライトのリスト</param>
	/// <param name="spriteList">数字スプライトのリスト</param>
	void updateImageNumbers (int value, List<Image> imageNumbers, List<Sprite> spriteList) {
		int size = imageNumbers.Count;
		// 表示上限計算
		value = Mathf.Min (value, (int)Mathf.Pow(10.0f,(size - 1)));
		bool displayZeroNumber = false;
		for(int index = size; index > 0; index--) {
			int num = value / (int)Mathf.Pow(10.0f,(index-1));
			if (num == 0 && !displayZeroNumber && index != 1) {
				imageNumbers[index-1].enabled = false;
			} else {
				displayZeroNumber = true;
				imageNumbers[index-1].enabled = true;
				imageNumbers[index-1].sprite = spriteList[num];
			}
			value = value % (int)Mathf.Pow(10.0f,(index-1));
		}
	}
}
