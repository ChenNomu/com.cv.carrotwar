#region 腳本說明
/*
 * 商標畫面腳本
 * 編輯者:陳穎駿
 * 最後編輯日期:2017/03/03
*/
#endregion
using UnityEngine;
using System.Collections;
using CommonManager;

public class LogoScene : MonoBehaviour
{
	///<summary>遊戲管理腳本</summary>
	private Game_Manager gameManager;

	public void Start ()
	{
		gameManager = GameObject.Find ("GameManager").GetComponent<Game_Manager> ();
	}
	///<summary>
	/// 移動至下一個場景
	/// </summary>
	public void ToNextScene ()
	{
		gameManager.LoadScene (SceneList.TitleScene);
	}
}
