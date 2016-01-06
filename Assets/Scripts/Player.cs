using UnityEngine;
using System.Collections;
/// <summary>
/// プレイヤークラス
/// 落下中のプレイヤーの制御
/// </summary>
public class Player : MonoBehaviour {

	[SerializeField] AudioManager audioManager = null;
	[SerializeField] ColorDirecter colorDirecter = null;
	[SerializeField] ParticleSystem cheakPointParticleSystem = null;
	public float boost { get; set; }
	public float addForce { get; set; }
	bool isGoal { get; set; }

	int chackDepthCounter = 1;

	// Use this for initialization
	void Start () {
		boost = GameDef.MAX_BOOST_VALUE;
	}
	
	// Update is called once per frame
	void Update () {
		// チェックポイント
		if(transform.position.y < GameDef.CHACK_DEPTH * chackDepthCounter) {
			audioManager.PlayOneShot (AudioManager.OneShotName.CLEAR);
			chackDepthCounter++;
			colorDirecter.ChangeFogColor ();
			cheakPointParticleSystem.Emit (GameDef.CHECK_POINT_EMIT_PARTICLE_NUM);
			if (checkGoal() && !isGoal) {
				Goal ();
			}
		}
	}

	/// <summary>
	/// 物理演算系の更新処理
	/// </summary>
	void FixedUpdate() {
		MovePlayer ();
	}

	/// <summary>
	/// プレイヤーの移動処理
	/// </summary>
	void MovePlayer ()
	{
		if (checkGoal ()) {
			GetComponent<Rigidbody> ().velocity = GetComponent<Rigidbody> ().velocity.y * Vector3.up;
			return;
		}
		// キー入力処理
		if (Controller.GetButtonDown(Controller.Button.FIRE)) {
			audioManager.PlayOneShot (AudioManager.OneShotName.BOOST);
		}
		if (Controller.GetButton(Controller.Button.FIRE)) {
			boost += GameDef.ADD_BOOST;
			addForce = GameDef.ADD_FORCE_POWER;
			if (boost > GameDef.MAX_BOOST_VALUE) {
				boost = GameDef.MAX_BOOST_VALUE;
				GetComponent<Rigidbody> ().AddForce (Vector3.down * GameDef.ADD_FORCE_POWER);
			}
			GetComponent<Rigidbody> ().drag = GameDef.MIN_DRAG_VALUE;
		}
		else {
			boost -= GameDef.REDUCE_BOOST;
			if (boost < 0.0f) {
				boost = 0.0f;
			}
			addForce = GameDef.ADD_FORCE_POWER_HIGH;
			GetComponent<Rigidbody> ().drag = GameDef.MAX_DRAG_VALUE;
		}
		if (Controller.GetButton(Controller.Button.UP)) {
			GetComponent<Rigidbody> ().AddForce (Vector3.forward * addForce);
		}
		if (Controller.GetButton(Controller.Button.DOWN)) {
			GetComponent<Rigidbody> ().AddForce (Vector3.back * addForce);
		}
		if (Controller.GetButton(Controller.Button.LEFT)) {
			GetComponent<Rigidbody> ().AddForce (Vector3.left * addForce);
		}
		if (Controller.GetButton(Controller.Button.RIGHT)) {
			GetComponent<Rigidbody> ().AddForce (Vector3.right * addForce);
		}
	}

	/// <summary>
	/// ゴール地点を超えているかをチェック
	/// </summary>
	/// <returns><c>true</c>, ゴール地点を超えている, <c>false</c> それ以外.</returns>
	bool checkGoal ()
	{
		return transform.position.y < GameDef.GOAL_DEPTH;
	}

	/// <summary>
	/// 他の物体との接触処理
	/// </summary>
	/// <param name="collision">接触したコリジョン</param>
	void OnCollisionEnter (Collision collision) {
		Debug.Log ("hit");
		boost -= GameDef.HIT_ENTER_REDUCE_BOOST;
		if (boost <= 0.0f) {
			GameOver ();
		} else {
			audioManager.PlayOneShot (AudioManager.OneShotName.DAMAGE);
		}
	}

	/// <summary>
	/// 他の物体のとの接触中処理
	/// </summary>
	/// <param name="collision">接触中のコリジョン</param>
	void OnCollisionStay(Collision collision) {
		Debug.Log ("hit");
		boost -= GameDef.HIT_STAY_REDUCE_BOOST;
		if (boost <= 0.0f) {
			GameOver ();
		} else {
			audioManager.PlayOneShot (AudioManager.OneShotName.DAMAGE);
		}
	}

	/// <summary>
	/// コリジョンとの接触処理
	/// </summary>
	/// <param name="collider">接触したコリジョン</param>
	void OnTriggerEnter(Collider collider) {
		Debug.Log ("Trigger");
		if( collider.gameObject.GetComponent<StageCip>() == null ) { return ; }
		audioManager.PlayOneShot (AudioManager.OneShotName.THROUGH);
		Destroy ( collider.gameObject, GameDef.CIP_DESTROY_WAIT_TIME );
	}

	/// <summary>
	/// ゲームオーバー時の処理
	/// </summary>
	void GameOver() {
		audioManager.PlayOneShot (AudioManager.OneShotName.CRASH);
		GameManager.instance.changeScene (GameDef.SceneId.SCORE);
		GameManager.instance.score = transform.position.y;
	}

	/// <summary>
	/// ゴール時の処理
	/// </summary>
	void Goal() {
		isGoal = true;
		StartCoroutine ( GoalDirection() );
	}

	/// <summary>
	/// ゴール演出
	/// </summary>
	/// <returns>コルーチン</returns>
	IEnumerator GoalDirection() {
		audioManager.PlayOneShot (AudioManager.OneShotName.GOAL);
		yield return new WaitForSeconds (GameDef.GOAL_DIRECTION_WAIT_TIME);
		GameManager.instance.changeScene (GameDef.SceneId.ENDING);
		GameManager.instance.score = transform.position.y;
	}
}