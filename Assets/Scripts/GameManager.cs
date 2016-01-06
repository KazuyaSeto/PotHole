using UnityEngine;
using System.Collections;
/// <summary>
/// Game manager.
/// シングルトン
/// ゲーム全体の管理、データ保持、シーン遷移など
/// </summary>
public class GameManager : MonoBehaviour {
	[SerializeField] SceneFadeInOut fader = null;

	// シングルトンインスタンス
	public static GameManager instance { get { return singletonInstnce; } }
	public float score { get; set; }

	static GameManager singletonInstnce = null;
	GameDef.SceneId loadingSceneId = GameDef.SceneId.INIT;

	void Awake () {
		singletonInstnce = this;
	}

	/// <summary>
	/// 破棄処理
	/// </summary>
	void OnDestory() {
		singletonInstnce = null;
	}

	// Use this for initialization
	void Start () {
		// ゲームの初期化処理
		DontDestroyOnLoad (gameObject);
		DontDestroyOnLoad (fader.gameObject);
		changeScene (GameDef.SceneId.TITLE);
	}



	// Update is called once per frame
	void Update () {

	}

	/// <summary>
	/// シーンを切り替え
	/// </summary>
	/// <param name="sceneId">シーンID</param>
	public void changeScene( GameDef.SceneId sceneId ) {
		if(loadingSceneId == sceneId) return ;
		loadingSceneId = sceneId; 
		fader.FadeIn ( () =>  {
			Application.LoadLevel( (int)sceneId );
			fader.FadeOut();
		});
	}
}
