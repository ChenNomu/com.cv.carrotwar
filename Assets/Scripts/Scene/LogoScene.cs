/*
 * 商標畫面腳本
 * 編輯者:陳穎駿
 * 最後編輯日期:2017/03/03
*/
using UnityEngine;
using System.Collections;
using CommonManager;

public class LogoScene : MonoBehaviour
{
	///<summary>遊戲管理腳本</summary>
	private Game_Manager gameManager;

	[SerializeField]
	private TweenAlpha _twAlpha;

	public void Start ()
	{
		gameManager = GameObject.Find ("GameManager").GetComponent<Game_Manager> ();

		// 增加Tween結束事件
		EventDelegate.Add (_twAlpha.onFinished, ToTitletScene);
	}

	///<summary>
	/// 移動至標題場景
	/// </summary>
	public void ToTitletScene ()
	{
		gameManager.LoadScene (SceneList.TitleScene);
	}
}
