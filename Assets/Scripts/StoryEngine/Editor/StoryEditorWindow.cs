/*
 * 故事編輯器腳本
 * 概要:
 * 製作故事撥放引擎用的JSON檔案。
 * 編輯者:陳穎駿
 * 最後編輯日期:2017/06/13
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
    ///<summary>故事功能對照列表</summary>
    Dictionary<string, Action<int>> _storyDic = new Dictionary<string, Action<int>>();

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
    ///<summary>滾軸座標</summary>
	private Vector2 scrollPosition;

	///<summary>存放路徑</summary>
	public static string _filePath = "";

	///<summary>在Unity工具列中新增功能</summary>
	[MenuItem("StoryEditor/Story Editor Window")]
	public static void CreateWindow ()
	{
		// @建立視窗
		m_window = EditorWindow.GetWindow (typeof(StoryEditorWindow)) as StoryEditorWindow;
        m_window.titleContent = new GUIContent("故事編輯器");

		// @設定視窗內為自動變更視窗中的內容
		m_window.autoRepaintOnSceneChange = true;

		// @固定視窗大小
		m_window.minSize = m_window.maxSize = new Vector2 (WindowSetting._windowSize.x, WindowSetting._windowSize.y);
	}

	///<summary>
	/// 視窗啟動
	/// </summary>
	void OnEnable()
	{
		InitSetting ();
		InitDialogue ();
		InitUIList ();
	}

	///<summary>
	/// 重置設定
	/// </summary>
	private void InitSetting ()
	{
		_editorMode = EditorMode.Null;
		_filePath = "";
	}

	///<summary>
	/// 重置Dialogue資料
	/// </summary>
	private void InitDialogue ()
	{
		_dialogue = new Dialogue ();
		_dialogue.storydata = new List<StoryData> ();
	}

	///<summary>
	/// 重置UI列表資料
	/// </summary>
	private void InitUIList ()
	{
		Command._key = new List<int> ();
		LoadFX._key = new List<int> ();
		SoundFX._key = new List<int> ();
		CharFX._key = new List<int> ();

        _storyDic.Add("load_fx", DataEdit.LoadFXView);
        _storyDic.Add("screen_fx", DataEdit.ScreenFXView);
        _storyDic.Add("sound_fx", DataEdit.SoundFXView);
        _storyDic.Add("char_fx", DataEdit.CharFXView);
        _storyDic.Add("text_out", DataEdit.TextOutView);
        _storyDic.Add("delay", DataEdit.DelayView);
        _storyDic.Add("tutorial", DataEdit.TutorialView);
        _storyDic.Add("end", DataEdit.EndView);
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
			EditorGUIUtility.labelWidth = UISetting._pathLabelW;
			EditorGUILayout.TextField ("檔案路徑：", _filePath, GUILayout.Width (UISetting._textFieldW));
		GUILayout.EndVertical ();

		GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("創新檔案", GUILayout.Width (UISetting._createStorybtnW))) 
    		{
                OpenSaveFilePanel ();
    		}

			if (GUILayout.Button ("讀取檔案", GUILayout.Width (UISetting._loadStoryBtnW)))
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
			scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(UISetting._scrollW), GUILayout.Height(UISetting._scrollH));
    		try{
    			// @顯示故事檔案中的每筆資料
			for (int count = 0; count < _dialogue.storydata.Count; ++count) 
    			{
				ShowScrollview (count);
    			}
    		}
    		catch(NullReferenceException error)
    		{
				ErrorMessage ("讀取失敗", "妳讀取的資料是空值！", "確定");
				Debug.LogError (error.Message);
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
		if (path.Length != DataSetting._zero) 
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
		CheckOpeningData (path);

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
            "讀取檔案",                             	// @標題
            "Assets/Resources/Dialogue",            // @儲存路徑
            "json");                                // @副檔名

		if (path.Length != DataSetting._zero)
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
		CheckOpeningData (path);

        // @讀取檔案
        StreamReader sr = new StreamReader (_filePath);
        string data = sr.ReadToEnd ();
        sr.Close ();

        // @檢查資料
        CheckData (data);
    }

    ///<summary>
    /// 從系統中讀取Json檔案
    /// </summary>
	private string LoadJson()
	{
		// @讀取檔案
		StreamReader sr = new StreamReader (_filePath);
		string data = sr.ReadToEnd ();
		sr.Close ();

		return data;
	}

    ///<summary>
    /// 檢查檔案資料
    /// </summary>
    ///<param name="data">讀取到的資料</param>
    private void CheckData (string data)
    {
        try
        {
            // @將資料轉換成JSON格式
            DataToJson (data);

            _editorMode = EditorMode.EditorMode;
        }
		catch (Exception error)
        {
			if (_dialogue == null)
			{
				InitDialogue ();
			}

			ErrorMessage ("讀取失敗", "資料不是正確的JSON格式!", "確定");
			Debug.LogError (error.Message);
        }
    }

	///<summary>
	/// 檢查是否有開啟中的檔案
	/// </summary>
	private void CheckOpeningData (string path)
	{
		if (_dialogue.storydata.Count > DataSetting._zero && !EditorUtility.DisplayDialog ("警告", "開啟中的檔案將被覆蓋！", "確定", "取消")) 
		{
			return;
		}

		// @設定檔案存放路徑
		_filePath = path;
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
		for(int index = 0; index < _dialogue.storydata.Count; ++index)
        {
			Command._key.Add(CheckJsonData(Command._command, _dialogue.storydata[index].command));
			LoadFX._key.Add (CheckJsonData(LoadFX._loadFX, _dialogue.storydata [index].parameter));
			ScreenFX._key.Add (CheckJsonData(ScreenFX._screenFX, _dialogue.storydata [index].parameter));
			SoundFX._key.Add(CheckJsonData(SoundFX._soundFX, _dialogue.storydata [index].parameter));
			CharFX._key.Add (CheckJsonData(CharFX._charFX, _dialogue.storydata [index].parameter));
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
		return (Array.IndexOf (indexList, data) == DataSetting._null) ? DataSetting._zero : Array.IndexOf (indexList, data);
    }

	///<summary>
	/// 初始化故事內容
	/// </summary>
	private StoryData InitStoryData ()
	{
        StoryData new_data = new StoryData();
        new_data.id = _dialogue.storydata.Count;
		new_data.command = Command._command[DataSetting._zero];
		new_data.parameter = LoadFX._loadFX[DataSetting._zero];
        new_data.parameter2 = "";
        return new_data;
	}

	///<summary>
	/// 建立新的故事內容
	/// </summary>
	private void CreateNewStoryData ()
	{
        _dialogue.storydata.Add(InitStoryData());

		Command._key.Add (DataSetting._zero);
		LoadFX._key.Add (DataSetting._zero);
		ScreenFX._key.Add (DataSetting._zero);
		SoundFX._key.Add (DataSetting._zero);
		CharFX._key.Add (DataSetting._zero);
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
    		if (GUILayout.Button ("刪除", GUILayout.Width(UISetting._deleteDataBtnW)))
    		{
    			RemoveData (key);
    		}

    		DataEdit.CommandView (key);

            if (_storyDic.ContainsKey(_dialogue.storydata[key].command))
            {
                _storyDic[_dialogue.storydata[key].command](key);
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

    	// @重新設定編號
		for (int index = 0; index < _dialogue.storydata.Count; ++index)
    	{
			_dialogue.storydata [index].id = index;
    	}

    	// @當資料為0筆資料時中斷顯示
		if (_dialogue.storydata.Count < DataSetting._lastData) 
    	{
    	    return;
    	}
	}

    ///<summary>
    /// 錯誤訊息視窗
    /// </summary>
    ///<param name="title">標題</param>
    ///<param name="msg">訊息內容</param>
    ///<param name="btn_text">按鈕文字</param>
	private void ErrorMessage (string title, string msg, string btn_text)
	{
		EditorUtility.DisplayDialog (title, msg, btn_text);
		InitSetting ();
		return;
	}
}