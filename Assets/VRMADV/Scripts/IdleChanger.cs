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
		GUI.Box(new Rect(100 , 45 ,120 , 830), "");
		if(GUI.Button(new Rect(110 , 60 ,100, 80), "Jab"))
			anim.SetBool ("Jab", true);
		if(GUI.Button(new Rect(110 , 140 ,100, 80), "Hikick"))
			anim.SetBool ("Hikick", true);
		if(GUI.Button(new Rect(110 , 220 ,100, 80), "Spinkick"))
			anim.SetBool ("Spinkick", true);
		if(GUI.Button(new Rect(110 , 300 ,100, 80), "Rising_P"))
			anim.SetBool ("Rising_P", true);
		if(GUI.Button(new Rect(110 , 380 ,100, 80), "Headspring"))
			anim.SetBool ("Headspring", true);
		if(GUI.Button(new Rect(110 , 460 ,100, 80), "Land"))
			anim.SetBool ("Land", true);
		if(GUI.Button(new Rect(110 , 540 ,100, 80), "ScrewKick"))
			anim.SetBool ("ScrewK", true);
		if(GUI.Button(new Rect(110 , 620 ,100, 80), "DamageDown"))
			anim.SetBool ("DamageDown", true);
		if(GUI.Button(new Rect(110 , 700 ,100, 80), "Run"))
			anim.SetBool ("Run", true);
		if(GUI.Button(new Rect(110 , 780 ,100, 80), "Run_end"))
			anim.SetBool ("Run", false);

;
	}
}
