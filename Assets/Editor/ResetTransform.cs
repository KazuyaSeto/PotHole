using UnityEngine;
using UnityEditor; // エディタスクリプトを利用するのに必要
using System.Collections;
[CustomEditor(typeof(Transform))] // 既存のTransformクラスを拡張する
public class ResetTransform : Editor { // Editorクラスを継承
	private Vector3 tempLocalPosition; // 代入前のlocalPositionを格納するVector3型変数
	private Vector3 tempLocalRotation; // 代入前のlocalRotationを格納するVector3型変数
	private Vector3 tempLocalScale;    // 代入前のlocalScaleを格納するVector3型変数

	public override void OnInspectorGUI () // TransformのInspectorのGUIをオーバーライド
	{
		Transform targetTransform = target as Transform; // ターゲットとなるTransform

		EditorGUI.BeginChangeCheck (); // BeginChangeCheck() ~ EndChangeCheck()間の値が変更されていないかをチェック

		// Begin Positionグループ
		// 水平に並べるメニューグループをBeginHorizontal() ~ EndHorizontal()間に入れる
		EditorGUILayout.BeginHorizontal(); //ここから

		// Vector3型の入力フィールドを作成(ラベル : Position、ターゲット : ターゲットのlocalPosition)
		tempLocalPosition = EditorGUILayout.Vector3Field("Position", targetTransform.localPosition);
		if (GUILayout.Button("Reset", GUILayout.Width(50f))){ // ラベルがResetのボタンを幅50fで作成。
			// リセットボタンがクリックされたらVector3(0f,0f,0f)をセット
			tempLocalPosition = Vector3.zero;
		}
		EditorGUILayout.EndHorizontal(); // ここまでが1グループ
		// End Positionグループ

		// Begin Rotationグループ
		EditorGUILayout.BeginHorizontal();
		tempLocalRotation = EditorGUILayout.Vector3Field("Rotation", targetTransform.localEulerAngles);
		if (GUILayout.Button("Reset", GUILayout.Width(50f))){
			tempLocalRotation = Vector3.zero;
		}
		EditorGUILayout.EndHorizontal();
		// End Rotationグループ

		// Begin Scaleグループ
		EditorGUILayout.BeginHorizontal();
		tempLocalScale = EditorGUILayout.Vector3Field("Scale", targetTransform.localScale);
		if (GUILayout.Button("Reset", GUILayout.Width(50f))){
			tempLocalScale = Vector3.one; // Scaleの初期値はVector3(1f,1f,1f)
		}
		EditorGUILayout.EndHorizontal();
		// End Scaleグループ

		if (EditorGUI.EndChangeCheck ()) { // BeginChangeCheck() ~ EndChangeCheck()間の値が変更されていた場合の処理
			Undo.RecordObject (target, "Undo Transform"); // アンドゥにターゲットの変更前の値を記録しておく
			targetTransform.localPosition = tempLocalPosition; // 変更された値をTransformにセットする
			targetTransform.localEulerAngles = tempLocalRotation;
			targetTransform.localScale = tempLocalScale;
		}
	}
}