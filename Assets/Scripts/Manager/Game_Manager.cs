#region 腳本說明
/*
 * 場景管理腳本
 * 概要:
 * 主要管理場景的讀取以及刪除不用的場景。
 * 目前因應專案需求，使用直接讀取本地端檔案的方式，往後可依照需求改為連線式的讀取方式。
 * 編輯者:陳穎駿
 * 最後編輯日期:2017/03/03
*/
#endregion
using UnityEngine;
using System;
using System.Collections;

namespace CommonManager
{
	public class Game_Manager : MonoBehaviour 
	{
		///<summary>目前的場景</summary>
		public static string _Scene;
		///<summary>上一個場景</summary>
		public static string back_Scene;

		//@ UI介面攝影機
		public static Transform _uiCamera;

		///<summary>場景檔路徑</summary>
		private string scene_Path = "Prefabs/Scenes/";

		void Awake ()
		{
			_uiCamera = GameObject.Find ("UI_Camera").transform;

			//LoadScene (SceneList.LogoScene);
		}

		void Start ()
		{
//#if RELEASE
			Debug.Log("Test");
			PlayerPrefs.DeleteAll ();
			LoadScene (SceneList.LogoScene);
//#endif
		}

		void Update () 
		{
//#if DEBUG_LOG
//			if (Input.GetKeyDown (KeyCode.F1))
//			{
//				LoadScene (SceneList.LogoScene);
//			}
//#endif
		}

		///<summary>
		/// 讀取場景
		/// </summary>
		///<param name="name">場景名稱</param>
		public void LoadScene (string name)
		{
			DestroyScene ();

			back_Scene = _Scene;
			_Scene = name;

			CreateScene (name);
		} 

		///<summary>
		/// 刪除場景
		/// </summary>
		private void DestroyScene()
		{
			try
			{
				DestroyObject (GameObject.FindGameObjectWithTag ("Scene"));
			}
			catch (NullReferenceException ex)
			{
				Debug.Log ("No game object called wibble found");
			}
		}

		///<summary>
		/// 實體化場景
		/// </summary>
		///<param name="name">場景名稱</param>
		private void CreateScene (string _name)
		{
#if DEBUG_LOG
			Debug.Log ((scene_Path + _name));
#endif
			GameObject scene = Instantiate (Resources.Load (scene_Path + _name)) as GameObject;

			InitScene (scene, _name);

			scene = null;
		}

		///<summary>
		/// 初始化場景
		/// </summary>
		///<param name="scene">場景物件</param>
		///<param name="_name">場景名稱</param>
		private void InitScene (GameObject scene , string _name)
		{
			scene.name = _name;
			scene.transform.parent = _uiCamera;
			scene.transform.localPosition = Vector3.zero;
			scene.transform.localRotation = new Quaternion (0, 0, 0, 0);
			scene.transform.localScale = new Vector3 (1, 1, 1);
		}
	}

	///<summary>
	/// 場景列表
	/// </summary>
	public class SceneList
	{
		///<summary>商標畫面</summary>
		public static string LogoScene = "LogoScene";
		///<summary>標題畫面</summary>
		public static string TitleScene = "TitleScene";
		///<summary>開頭動畫</summary>
		public static string OpenningScene = "OpenningScene";
		///<summary>大地圖畫面</summary>
		public static string MainScene = "MainScene";
		///<summary>商店畫面</summary>
		public static string ShopScene = "ShopScene";
		///<summary>關卡畫面</summary>
		public static string BattleScene = "BattleScene";
		///<summary>讀取畫面</summary>
		public static string LoadScene = "LoadScene";
	}
}
