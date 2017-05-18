/*
 * 讀取畫面腳本
 * 編輯者:陳穎駿
 * 最後編輯日期:2017/05/07
*/
using UnityEngine;
using System;
using System.Collections;
using CommonManager;

public class LoadScene : MonoBehaviour
{
	private Game_Manager gameManager;
	///<summary>下一個場景名稱</summary>
	public static string next_Scene;

	void Start ()
	{
		ToNextScene ();
	}

	///<summary>
	/// 移動至下一個場景
	/// </summary>
	private void ToNextScene ()
	{
		try
		{
			gameManager.LoadScene (next_Scene);
		}
		catch (NullReferenceException ex)
		{
			Debug.LogError ("next_Scene is null!");
		}
	}
}
