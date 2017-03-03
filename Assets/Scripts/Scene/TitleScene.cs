#region 腳本說明
/*
 * 標題畫面腳本
 * 編輯者:陳穎駿
 * 最後編輯日期:2017/03/03
*/
#endregion
using UnityEngine;
using System.Collections;
using CommonManager;

public class TitleScene : MonoBehaviour
{
	///<summary>遊戲管理腳本</summary>
	private Game_Manager gameManager;

	[SerializeField]
	private TweenPosition _twPosition;
	[SerializeField]
	private TweenAlpha _twAlpha;

	public void Start ()
	{
		gameManager = GameObject.Find ("GameManager").GetComponent<Game_Manager> ();
	}

	///<summary>
	/// 移動至下一個場景
	/// </summary>
	public void ToOpeningScene ()
	{
		gameManager.LoadScene (SceneList.OpenningScene);
	}

	///<summary>
	/// 移動至下一個場景
	/// </summary>
	///<param name="scene">場景名稱</param>
	public void ToNextScene (string scene)
	{
		gameManager.LoadScene (scene);
	}

	///<summary>
	/// 畫面點擊事件
	/// </summary>
	public void OnClick ()
	{
		if (PlayerPrefs.HasKey ("is_first_play")) 
		{
			// todo. 設定在讀取畫面中要讀取的資料類型

			ToNextScene(SceneList.LoadScene);
		}
		else
		{
			TweenControl (true);
			PlayerPrefs.SetString ("is_first_play", "true");
		}
	}

	///<summary>
	/// 控制Tween開關
	/// </summary>
	///<param name="flag">開關設定 true:開 false:關</param>
	public void TweenControl(bool flag)
	{
		_twAlpha.enabled = flag;
		_twPosition.enabled = flag;
	}
}
