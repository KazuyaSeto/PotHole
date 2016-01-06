using UnityEngine;
using System.Collections;
/// <summary>
/// ゲーム内全体で使う定義
/// </summary>
public class GameDef {
	/// シーンの定義
	/// シーン間の移動に使う。
	/// unityのシーンidと一致させる必要がある。
	/// unityのシーンidはBuildSettingsで確認できる。
	public enum SceneId : int {
		INIT = 0,
		TITLE = 1,
		GAME = 2,
		SCORE = 3,
		ENDING = 4
	};

	// レベルデザインに必要なパラメータ

	// ゴール地点の深さ設定
	public static readonly int GOAL_DEPTH = -10000; // ゴール地点の深さ
	public static readonly int CHACK_DEPTH = -1000; // チェックポイントの感覚

	// プレイヤー挙動
	public static readonly float ADD_FORCE_POWER = 3.0f;
	public static readonly float ADD_FORCE_POWER_HIGH = 10.0f;
	public static readonly float MAX_BOOST_VALUE = 1.0f;
	public static readonly float MAX_DRAG_VALUE = 2.0f; // 加速時の空気抵抗
	public static readonly float MIN_DRAG_VALUE = 0.02f; // 減速時の空気抵抗
	public static readonly float ADD_BOOST = 0.001f; // 加速時のブースト増加量
	public static readonly float REDUCE_BOOST = 0.01f; // 減速時のブースト使用量
	public static readonly float HIT_ENTER_REDUCE_BOOST = 0.1f; // 減速時のブースト使用量
	public static readonly float HIT_STAY_REDUCE_BOOST = 0.01f; // 減速時のブースト使用量

	// 演出系パラメータ
	public static readonly float GOAL_DIRECTION_WAIT_TIME = 5.0f; // ゴール演出時の待ち時間
	public static readonly int CHECK_POINT_EMIT_PARTICLE_NUM = 1000; // チェックポイント通過時の放出数パーティクル数

	// ステージ生成系パラメータ
	public static readonly float CIP_DESTROY_WAIT_TIME = 5.0f; // ゴール演出時の待ち時間
	public static readonly int TONNEL_LOOP_SIZE = 10; // 外壁のループサイズ
	public static readonly float STAGE_START_DEPTH = -100.0f;
	// マップ生成パターン
	public enum StageCipGeneratedPattern {
		NONE, // なし
		LINER, // 一定の回転角度
		PING_PONG, // 途中で回転角度を反転する
		RANDOM // ランダムな回転角度で生成
	};
}
