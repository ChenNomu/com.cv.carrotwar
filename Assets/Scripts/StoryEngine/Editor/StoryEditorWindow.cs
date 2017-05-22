﻿/*
 * 故事編輯器腳本
 * 概要:
 * 製作故事撥放引擎用的JSON檔案。
 * 編輯者:陳穎駿
 * 最後編輯日期:2017/05/21
*/
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class StoryEditorWindow : EditorWindow
{
	///<summary>編輯視窗</summary>
	public static StoryEditorWindow m_window = null;

	///<summary>視窗中的模式列表</summary>
	enum EditorMode 
    {
		Null,
		EditorMode,
	}

	///<summary>當前的模式</summary>
	static EditorMode _editorMode = EditorMode.Null;
	///<summary>故事腳本</summary>
	public static Dialogue _dialogue = new Dialogue();

	private Vector2 scrollPosition;

	///<summary>存放路徑</summary>
	public static string _filePath = "";

	///<summary>在Unity工具列中新增功能</summary>
	[MenuItem("StoryEditor/Story Editor Window")]
	public static void CreateWindow ()
	{
		// @建立視窗
		m_window = EditorWindow.GetWindow (typeof(StoryEditorWindow)) as StoryEditorWindow;

		m_window.title = "故事編輯器";

		// @自動變更視窗中的內容
		m_window.autoRepaintOnSceneChange = true;

		// @固定視窗大小
		m_window.minSize = m_window.maxSize = new Vector2 (800, 600);
	}

	///<summary>
	/// 視窗啟動
	/// </summary>
	void OnEnable()
	{
		_editorMode = EditorMode.Null;
		_filePath = "";
		_dialogue = new Dialogue ();
		_dialogue.storydata = new List<StoryData> ();
		Command._key = new List<int> ();
		LoadFX._key = new List<int> ();
		SoundFX._key = new List<int> ();
		CharFX._key = new List<int> ();
		ButtonFX._key = new List<int> ();
	}

	void OnGUI ()
	{
		Menu ();

        // @只在編輯模式中才顯示內容
		if (_editorMode != EditorMode.Null) 
		{
			ShowStory ();
		}
	}

	///<summary>
	/// 公用選單
	/// </summary>
	void Menu ()
	{
		// @顯示檔案路徑
		GUILayout.BeginVertical ();
		    EditorGUIUtility.labelWidth = 60.0f;
		    EditorGUILayout.TextField ("檔案路徑：", _filePath, GUILayout.Width (700.0f));
		GUILayout.EndVertical ();

		GUILayout.BeginHorizontal ();
    		if (GUILayout.Button ("創新檔案", GUILayout.Width (80.0f))) 
    		{
                OpenSaveFilePanel ();
    		}

    		if (GUILayout.Button ("讀取JSON檔案", GUILayout.Width (120.0f)))
    		{
    			OpenLoadFilePanel ();
    		}
		GUILayout.EndHorizontal ();
	}

	///<summary>
	/// 顯示故事內容
	/// </summary>
	void ShowStory ()
	{
		GUILayout.BeginHorizontal ();
    		if (GUILayout.Button ("新增一筆資料")) 
    		{
    			// @新增一條內容
    			CreateNewStoryData();
    		}
    		if (GUILayout.Button ("清除所有資料")) 
    		{
    			// @清除所有資料
    			ClearAllStoryData();
    		}
    		if (GUILayout.Button ("儲存"))
            {
    			// @存檔
    			SaveStoryData ();
    		}
		GUILayout.EndHorizontal ();

    	scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(800), GUILayout.Height(530));
    		try{
    			// @顯示故事檔案中的每筆資料
    			for (int i = 0; i < _dialogue.storydata.Count; ++i) 
    			{
    				ShowScrollview (i);
    			}
    		}
    		catch(NullReferenceException error)
    		{
    			EditorUtility.DisplayDialog ("讀取失敗", "妳讀取的資料是空值！", "確定");
    			_editorMode = EditorMode.Null;

    		}
		GUILayout.EndScrollView();
	}

	///<summary>
	/// 開啟儲存檔案視窗
	/// </summary>
	private void OpenSaveFilePanel ()
	{
		// @開啟儲存視窗
		var path = EditorUtility.SaveFilePanel(
			"創建檔案",								// @標題
			"Assets/Resources/Dialogue",			// @儲存路徑
			"",										// @檔名
			"json");								// @副檔名

		// @檢查路徑
		if (path.Length != 0) 
        {
            // @創新故事
            CreateStory (path);
		}
	}

    ///<summary>
    /// 建立新的故事
    /// </summary>
    ///<param name="path">檔案路徑</param>
    private void CreateStory (string path)
    {
        if (_dialogue.storydata.Count > 0 && !EditorUtility.DisplayDialog ("警告", "開啟中的檔案將被覆蓋！", "確定", "取消")) 
        {
            return;
        }

        // @設定檔案存放路徑
        _filePath = path;

        // @將資料全部清除
        ClearAllStoryData ();

        // @創建空檔案
        File.WriteAllText (_filePath, JsonUtility.ToJson(_dialogue));

        // @切換模式
        _editorMode = EditorMode.EditorMode;

        // @刷新列表
        AssetDatabase.Refresh ();
    }

    ///<summary>
    /// 開啟讀取視窗
    /// </summary>
    private void OpenLoadFilePanel ()
    {
        // @開啟儲存視窗
        var path = EditorUtility.OpenFilePanel(
            "創建檔案",                             // @標題
            "Assets/Resources/Dialogue",            // @儲存路徑
            "json");                                // @副檔名

        if (path.Length != 0)
        {
            // @讀取故事
            LoadStory (path);
        }
    }

    ///<summary>
    /// 讀取故事檔案
    /// </summary>
    ///<param name="path">檔案路徑</param>
    private void LoadStory (string path)
    {
        if (_dialogue.storydata.Count > 0 && !EditorUtility.DisplayDialog ("警告", "開啟中的檔案將被覆蓋！", "確定", "取消")) 
        {
            return;
        }

        _filePath = path;

        // @讀取檔案
        StreamReader sr = new StreamReader (_filePath);
        string data = sr.ReadToEnd ();
        sr.Close ();

        // @檢查資料
        CheckData (data);
    }

    ///<summary>
    /// 檢查檔案資料
    /// </summary>
    ///<param name="data">讀取到的資料</param>
    private void CheckData (string data)
    {
        // @當資料為空值時顯示錯誤訊息並清除檔案路徑
        if (data == "") 
        {
            EditorUtility.DisplayDialog ("讀取失敗", "資料為空的！", "確定");
            _filePath = "";
            return;
        }

        try
        {
            // @將資料轉換成JSON格式
            DataToJson (data);

            _editorMode = EditorMode.EditorMode;
        }
        catch (ArgumentException error)
        {
            EditorUtility.DisplayDialog ("讀取失敗", "資料不是正確的JSON格式!", "確定");
            _filePath = "";
        }
    }

    ///<summary>
    /// 轉換資料格式
    /// </summary>
    ///<param name="data">讀取到的資料</param>
    private void DataToJson (string data)
    {
        // @字串格式轉JSON格式
        _dialogue = JsonUtility.FromJson<Dialogue> (data);

        // @建立顯示用資料
        for(int i = 0; i < _dialogue.storydata.Count; ++i)
        {
			Command._key.Add(CheckJsonData(Command._command, _dialogue.storydata[i].command));
			LoadFX._key.Add (CheckJsonData(LoadFX._loadFX, _dialogue.storydata [i].parameter));
			ScreenFX._key.Add (CheckJsonData(ScreenFX._screenFX, _dialogue.storydata [i].parameter));
            SoundFX._key.Add(CheckJsonData(SoundFX._soundFX, _dialogue.storydata [i].parameter));
			CharFX._key.Add (CheckJsonData(CharFX._charFX, _dialogue.storydata [i].parameter));
			ButtonFX._key.Add (CheckJsonData(ButtonFX._buttonFX, _dialogue.storydata [i].parameter));
        }
    }

    ///<summary>
    /// 檢查資料key值
    /// </summary>
    ///<param name="indexList">檢索用列表</param>
    ///<param name="data">索引值</param>
    private int CheckJsonData (string[] indexList, string data)
    {
        // @檢查不到資料時將索引值設定為0，檢查到資料時將索引值回傳
        return (Array.IndexOf (indexList, data) == -1) ? 0 : Array.IndexOf (indexList, data);
    }

	///<summary>
	/// 初始化故事內容
	/// </summary>
	private StoryData InitStoryData ()
	{
        StoryData new_data = new StoryData();
        new_data.id = _dialogue.storydata.Count;
        new_data.command = Command._command[0];
        new_data.parameter = LoadFX._loadFX[0];
        new_data.parameter2 = "";
        return new_data;
	}

	///<summary>
	/// 建立新的故事內容
	/// </summary>
	private void CreateNewStoryData ()
	{
        _dialogue.storydata.Add(InitStoryData());

		Command._key.Add (0);
		LoadFX._key.Add (0);
		ScreenFX._key.Add (0);
		SoundFX._key.Add (0);
		CharFX._key.Add (0);
		ButtonFX._key.Add (0);
	}

	///<summary>
	/// 清除所有故事內容
	/// </summary>
	private void ClearAllStoryData ()
	{
		_dialogue.storydata.Clear ();

		Command._key.Clear ();
		LoadFX._key.Clear ();
		ScreenFX._key.Clear ();
		SoundFX._key.Clear ();
		CharFX._key.Clear ();
		ButtonFX._key.Clear ();
	}

	///<summary>
	/// 儲存故事內容
	/// </summary>
	private void SaveStoryData ()
	{
		string _json = JsonUtility.ToJson(_dialogue);

        // @重新排版
		_json = _json.Replace ("},{", "},\n{");
		_json = _json.Replace (":[{", ":[\n{");

		File.WriteAllText(_filePath, _json);

		AssetDatabase.Refresh ();
	}

	///<summary>
	/// 顯示列表
	/// </summary>
    ///<param name="key">資料的Key值</param>
	private void ShowScrollview (int key)
	{
		GUILayout.BeginHorizontal(GUI.skin.box);
    		if (GUILayout.Button ("刪除", GUILayout.Width(50.0f)))
    		{
    			RemoveData (key);
    		}

    		DataEdit.CommandView (key);

    		switch (_dialogue.storydata[key].command) 
    		{
    		    case "load_fx":
    			    DataEdit.LoadFXView (key);
    	            break;
                case "screen_fx":
                    DataEdit.ScreenFXView(key);
    	            break;
                case "sound_fx":
                    DataEdit.SoundFXView(key);
        			break;
    			case "char_fx":
    				DataEdit.CharFXView (key);
        			break;
        		case "text_out":
    				DataEdit.TextOutView (key);
        			break;
        		case "delay":
    				DataEdit.DelayView (key);
        			break;
        		case "button_fx":
    				DataEdit.ButtonFXView (key);
        			break;
        		case "control_fx":
    				DataEdit.ControlFXView (key);
        			break;
        		case "end":
    				DataEdit.EndView (key);
        			break;
    		}
		GUILayout.EndHorizontal();
	}

    ///<summary>
    /// 刪除單筆資料
    /// </summary>
    ///<param name="key">資料的Key值</param>
	private void RemoveData (int key)
	{
    	_dialogue.storydata.RemoveAt (key);

		Command._key.RemoveAt (key);
		LoadFX._key.RemoveAt (key);
		ScreenFX._key.RemoveAt (key);
		SoundFX._key.RemoveAt (key);
		CharFX._key.RemoveAt (key);
		ButtonFX._key.RemoveAt (key);

    	// @重新設定編號
    	for (int i = 0; i < _dialogue.storydata.Count; ++i)
    	{
    	    _dialogue.storydata [i].id = i;
    	}

    	// @當資料為0筆資料時中斷顯示
    	if (_dialogue.storydata.Count < 1) 
    	{
    	    return;
    	}
	}
}