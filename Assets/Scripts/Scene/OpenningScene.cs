/*
 * 片頭畫面腳本
 * 編輯者:陳穎駿
 * 最後編輯日期:2017/05/06
*/
using UnityEngine;
using System.Collections;
using CommonManager;

public class OpenningScene : MonoBehaviour
{
	///<summary>遊戲管理腳本</summary>
	private Game_Manager gameManager;

	[SerializeField]
	private UIButton _uiButton;

	void Start ()
	{
		gameManager = GameObject.Find ("GameManager").GetComponent<Game_Manager> ();

		EventDelegate.Add (_uiButton.onClick, ToLoadScene);
	}

	///<summary>
	/// 移動至讀取場景
	/// </summary>
	public void ToLoadScene ()
	{
		// TODO. 需加上設定讀取完後的場景
		gameManager.LoadScene (SceneList.LoadScene);
	}
}
