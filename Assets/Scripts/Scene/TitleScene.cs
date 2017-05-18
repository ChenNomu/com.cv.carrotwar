﻿/*
 * 標題畫面腳本
 * 編輯者:陳穎駿
 * 最後編輯日期:2017/05/18
*/
using UnityEngine;
using System.Collections;
using CommonManager;

public class TitleScene : MonoBehaviour
{
	private Game_Manager gameManager;

	[SerializeField]
	private TweenPosition _twPosition;
	[SerializeField]
	private TweenAlpha _twAlpha;
	[SerializeField]
	private UIButton _uiButton;

	void Start ()
	{
		gameManager = GameObject.Find ("GameManager").GetComponent<Game_Manager> ();
		EventDelegate.Add (_uiButton.onClick, PlayAnimation);
	}

	///<summary>
	/// 播放動畫
	/// </summary>
	private void PlayAnimation ()
	{
		// TODO. 若要做成連線機制，在此傳送登入資料給伺服器

		//@ 執行Tween
		TweenControl (true);

		//@ 依照狀況增加Tween結束事件
		if (PlayerPrefs.HasKey (PlayerPrefsKey_Manager.First_Play))
		{
			EventDelegate.Add (_twAlpha.onFinished, ToLoadScene);
		}
		else
		{
			EventDelegate.Add (_twAlpha.onFinished, ToOpeningScene);

			//@ 儲存玩家遊戲進度
			PlayerPrefs.SetString (PlayerPrefsKey_Manager.First_Play, "true");
		}
	}

	///<summary>
	/// 移動至讀取場景
	/// </summary>
	private void ToLoadScene ()
	{
		// TODO. 需加上設定讀取完後的場景
		gameManager.LoadScene (SceneList.LoadScene);
	}

	///<summary>
	/// 移動至片頭動畫場景
	/// </summary>
	private void ToOpeningScene ()
	{
		// TODO. 需加上設定讀取完後的場景
		gameManager.LoadScene (SceneList.OpenningScene);
	}

	///<summary>
	/// 控制Tween開關
	/// </summary>
	///<param name="flag">開關設定 true:開 false:關</param>
	private void TweenControl(bool flag)
	{
		_twAlpha.enabled = flag;
		_twPosition.enabled = flag;
	}
}
