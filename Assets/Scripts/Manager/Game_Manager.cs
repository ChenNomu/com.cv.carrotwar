/*
 * 場景管理腳本
 * 概要:
 * 主要管理場景的讀取以及刪除不用的場景。
 * 目前因應專案需求，使用直接讀取本地端檔案的方式，往後可依照需求改為連線式的讀取方式。
 * 編輯者:陳穎駿
 * 最後編輯日期:2017/05/07
*/
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

		public static Transform _uiCamera;

		// TODO. 新增彈跳視窗和訊息視窗

		///<summary>場景檔路徑</summary>
		private string scene_Path = "Prefabs/Scenes/";

		void Awake ()
		{
			_uiCamera = GameObject.Find ("UI_Camera").transform;
		}

		void Start ()
		{
#if DEVELOP
			PlayerPrefs.DeleteAll ();
#elif RELEASE
			LoadScene (SceneList.LogoScene);
#endif
		}

		void Update () 
		{
#if DEVELOP
			if (Input.GetKeyDown (KeyCode.F1))
			{
				LoadScene (SceneList.LogoScene);
			}
#endif
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
			catch (ArgumentException ex)
			{
				Debug.LogError ("Can't find the object!");
				return;
			}
		}

		///<summary>
		/// 實體化場景
		/// </summary>
		///<param name="name">場景名稱</param>
		private void CreateScene (string _name)
		{
			GameObject scene;
			try
			{
				scene = Instantiate (Resources.Load (scene_Path + _name)) as GameObject;
			}
			catch (ArgumentException ex)
			{
				Debug.LogError ("Can't Instantiate the object! object name: " + scene_Path + _name);
				return;
			}

			InitScene (scene, _name);

			scene = null;
#if DEBUG_LOG
			Debug.Log(LogMessage("green", "Load Scene: " + _name));
#endif
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

		///<summary>
		/// 開啟彈跳視窗
		/// </summary>
		private void OpenPopup ()
		{
			// TODO. 開啟彈跳視窗
		}

		///<summary>
		/// 關閉彈跳視窗
		/// </summary>
		private void ClosePopup ()
		{
			// TODO. 關閉彈跳視窗
		}

		///<summary>
		/// 開啟訊息視窗
		/// </summary>
		///<param name="message">訊息</param>
		///<param name="button_type">按鈕類型(0只有close,1只有ok,2同時有ok和cencel)</param>
		///<param name="ok_action">ok按鈕功能</param>
		private void OpenMessage (string message, int button_type, Action ok_action = null)
		{
			// TODO. 設定訊息及按鈕類型、依照按鈕類型設定按鈕功能
		}

		///<summary>
		/// 關閉視窗
		/// </summary>
		private void CloseMessage ()
		{
			// TODO. 關閉訊息視窗
		}

		///<summary>
		/// 設定Log訊息
		/// </summary>
		///<param name="color">顏色</param>
		///<param name="messge">訊息</param>
		public string LogMessage (string color, string messge)
		{
			return "<color=" + color + ">" + messge + "</color>";
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
