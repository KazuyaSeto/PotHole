using UnityEngine;
using System.Collections;
/// <summary>
/// ステージを構成する障害物
/// 障害物生成に必要なデータセット
/// unityのインスペクター上から設定する
/// </summary>
public class StageCip : MonoBehaviour {
	public int depth = 10; // 障害物の一つ分の深さ
	public int initialValue = 0; // 障害物の生成の初期角度
	public int rotateValue = 45; // 障害物の生成回転角度
	public int repeatCount = 1; // 障害物の生成回数
	public int coolDepth = 50; // 障害物生成後の空白の深さ
	public GameDef.StageCipGeneratedPattern generatedPattern = GameDef.StageCipGeneratedPattern.LINER;

	/// <summary>
	/// 障害物の合計の深さを取得
	/// </summary>
	/// <value>The total depth.</value>
	public int totalDepth {
		get {
			return depth * repeatCount + coolDepth;
		}
	}
}
