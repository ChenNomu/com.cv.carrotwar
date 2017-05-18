/*
 * 片頭畫面腳本
 * 編輯者:陳穎駿
 * 最後編輯日期:2017/05/18
*/
using UnityEngine;
using System.Collections;
using CommonManager;

public class OpenningScene : MonoBehaviour
{
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
	private void ToLoadScene ()
	{
		// TODO. 需加上設定讀取完後的場景
		gameManager.LoadScene (SceneList.LoadScene);
	}
}
