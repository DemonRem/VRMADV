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
		GUI.Box(new Rect(100 , 45 ,120 , 440), "");
		if(GUI.Button(new Rect(110 , 60 ,100, 40), "Jab"))
			anim.SetBool ("Jab", true);
		if(GUI.Button(new Rect(110 , 100 ,100, 40), "Hikick"))
			anim.SetBool ("Hikick", true);
		if(GUI.Button(new Rect(110 , 140 ,100, 40), "Spinkick"))
			anim.SetBool ("Spinkick", true);
		if(GUI.Button(new Rect(110 , 180 ,100, 40), "Rising_P"))
			anim.SetBool ("Rising_P", true);
		if(GUI.Button(new Rect(110 , 220 ,100, 40), "Headspring"))
			anim.SetBool ("Headspring", true);
		if(GUI.Button(new Rect(110 , 260 ,100, 40), "Land"))
			anim.SetBool ("Land", true);
		if(GUI.Button(new Rect(110 , 300 ,100, 40), "ScrewKick"))
			anim.SetBool ("ScrewK", true);
		if(GUI.Button(new Rect(110 , 340 ,100, 40), "DamageDown"))
			anim.SetBool ("DamageDown", true);
		if(GUI.Button(new Rect(110 , 380 ,100, 40), "Run"))
			anim.SetBool ("Run", true);
		if(GUI.Button(new Rect(110 , 420 ,100, 40), "Run_end"))
			anim.SetBool ("Run", false);

;
	}
}
