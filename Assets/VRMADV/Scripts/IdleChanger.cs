using UnityEngine;
using System.Collections;

//
// アニメーション簡易プレビュースクリプト
// 2015/4/25 quad arrow
//

// Require these components when using this script
[RequireComponent(typeof(Animator))]

public class IdleChanger : MonoBehaviour
{

	private Animator anim;						// Animatorへの参照
	private AnimatorStateInfo currentState;		// 現在のステート状態を保存する参照
	private AnimatorStateInfo previousState;	// ひとつ前のステート状態を保存する参照


	// Use this for initialization
	void Start ()
	{
		// 各参照の初期化
		anim = GetComponent<Animator> ();
		currentState = anim.GetCurrentAnimatorStateInfo (0);
		previousState = currentState;

	}



	void OnGUI()
	{	
		int distance = 240;
		int diff = 80;
		GUI.Box(new Rect(80, distance, 120, 830), "");
		distance += 15;
		if(GUI.Button(new Rect(90 , distance,100, 80), "Jab"))
			anim.SetBool ("Jab", true);
		distance += diff;
		if(GUI.Button(new Rect(90 , distance,100, 80), "Hikick"))
			anim.SetBool ("Hikick", true);
		distance += diff;
		if(GUI.Button(new Rect(90 , distance ,100, 80), "Spinkick"))
			anim.SetBool ("Spinkick", true);
		distance += diff;
		if(GUI.Button(new Rect(90 , distance ,100, 80), "Rising_P"))
			anim.SetBool ("Rising_P", true);
		distance += diff;
		if(GUI.Button(new Rect(90 , distance ,100, 80), "Headspring"))
			anim.SetBool ("Headspring", true);
		distance += diff;
		if(GUI.Button(new Rect(90 , distance ,100, 80), "Land"))
			anim.SetBool ("Land", true);
		distance += diff;
		if(GUI.Button(new Rect(90 , distance ,100, 80), "ScrewKick"))
			anim.SetBool ("ScrewK", true);
		distance += diff;
		if(GUI.Button(new Rect(90 , distance ,100, 80), "DamageDown"))
			anim.SetBool ("DamageDown", true);
		distance += diff;
		if(GUI.Button(new Rect(90 , distance ,100, 80), "Run"))
			anim.SetBool ("Run", true);
		distance += diff;
		if(GUI.Button(new Rect(90 , distance ,100, 80), "Run_end"))
			anim.SetBool ("Run", false);
	}
}
