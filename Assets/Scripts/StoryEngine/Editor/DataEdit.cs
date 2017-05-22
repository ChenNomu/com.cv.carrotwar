/*
 * 故事項目設定腳本
 * 編輯者:陳穎駿
 * 最後編輯日期:2017/05/22
*/
using UnityEngine;
using UnityEditor;
using System.Collections;

public class DataEdit : EditorWindow
{
    ///<summary>項目文字</summary>
    private static string _label;

	///<summary>
	/// 顯示下拉式選單
	/// </summary>
	///<param name="label">標題文字</param>
	///<param name="index">索引值</param>
	///<param name="display">顯示用列表</param>
	///<param name="label_width">標題文字的寬度</param>
	/// <param name="width">顯示用列表的寬度</param>
	private static int ShowPopup (string label, int index, string[] display, float label_width, float width)
	{
		EditorGUIUtility.labelWidth = label_width;
		return EditorGUILayout.Popup (label, index, display, GUILayout.Width(width));
	}

	///<summary>
	/// 設定輸入框
	/// </summary>
	///<param name="label">標題文字</param>
	///<param name="textfield">輸入框用文字</param>
	///<param name="label_width">標題文字的寬度</param>
	/// <param name="width">輸入框的寬度</param>
	private static string ShowTextField (string label, string textfield, float label_width, float width)
	{
		EditorGUIUtility.labelWidth = label_width;
		return EditorGUILayout.TextField (label, textfield, GUILayout.Width (width));
	}

    ///<summary>
    /// 功能設定列表
    /// </summary>
    ///<param name="key">資料索引值</param>
	public static void CommandView (int key)
	{
		Command._key [key] = ShowPopup ("功能", Command._key [key], Command._index, 30.0f, 120.0f);
		StoryEditorWindow._dialogue.storydata[key].command = Command._command[Command._key[key]];
	}

    ///<summary>
    /// 讀取設定列表
    /// </summary>
    ///<param name="key">資料索引值</param>
    public static void LoadFXView (int key)
	{
		GUILayout.BeginVertical ();
			GUILayout.BeginHorizontal ();
				LoadFX._key [key] = ShowPopup("指令", LoadFX._key [key], LoadFX._index, 30.0f, 100.0f);
				StoryEditorWindow._dialogue.storydata [key].parameter = LoadFX._loadFX [LoadFX._key [key]];
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
				StoryEditorWindow._dialogue.storydata [key].parameter2 = ShowTextField (LoadFX._paramIndex [LoadFX._key [key]],
					StoryEditorWindow._dialogue.storydata [key].parameter2, 
					50.0f,
					350.0f);
			GUILayout.EndHorizontal ();
		GUILayout.EndVertical();
	}

    ///<summary>
    /// 畫面設定列表
    /// </summary>
    ///<param name="key">資料索引值</param>
    public static void ScreenFXView (int key)
    {
        GUILayout.BeginVertical ();
            GUILayout.BeginHorizontal ();       
				ScreenFX._key [key] = ShowPopup ("指令", ScreenFX._key [key], ScreenFX._index, 30.0f, 100.0f);
				StoryEditorWindow._dialogue.storydata [key].parameter = ScreenFX._screenFX [ScreenFX._key [key]];
            GUILayout.EndHorizontal ();

            GUILayout.BeginHorizontal ();
				StoryEditorWindow._dialogue.storydata [key].parameter2 = ShowTextField (ScreenFX._paramIndex [ScreenFX._key [key]],
					StoryEditorWindow._dialogue.storydata [key].parameter2,
					ScreenFX._labelWidth [ScreenFX._key [key]],
					350.0f);
			GUILayout.EndHorizontal ();
        GUILayout.EndVertical();
    }

    ///<summary>
    /// 音源設定列表
    /// </summary>
    ///<param name="key">資料索引值</param>
    public static void SoundFXView (int key)
    {
        GUILayout.BeginVertical ();
            GUILayout.BeginHorizontal (); 
				SoundFX._key [key] = ShowPopup ("指令：", SoundFX._key [key], SoundFX._index, 30.0f, 100.0f);
				StoryEditorWindow._dialogue.storydata[key].parameter = SoundFX._soundFX[SoundFX._key[key]];
            GUILayout.EndHorizontal ();

            GUILayout.BeginHorizontal ();
				if (SoundFX._key[key] != 4)
                {
					StoryEditorWindow._dialogue.storydata [key].parameter2 = ShowTextField (SoundFX._paramIndex [SoundFX._key [key]],
						StoryEditorWindow._dialogue.storydata [key].parameter2,
						SoundFX._labelWidth [SoundFX._key [key]],
						350.0f);
                }
            GUILayout.EndHorizontal ();
        GUILayout.EndVertical();
    }

    ///<summary>
    /// 角色設定列表
    /// </summary>
    ///<param name="key">資料索引值</param>
	public static void CharFXView (int key)
	{
		GUILayout.BeginVertical ();
			GUILayout.BeginHorizontal (); 
				CharFX._key [key] = ShowPopup ("指令：", CharFX._key [key], CharFX._index, 30.0f, 130.0f);
				StoryEditorWindow._dialogue.storydata[key].parameter = CharFX._charFX[CharFX._key [key]];
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
				StoryEditorWindow._dialogue.storydata [key].parameter2 = ShowTextField (CharFX._paramIndex [CharFX._key [key]],
					StoryEditorWindow._dialogue.storydata [key].parameter2,
					50.0f,
					350.0f);
			GUILayout.EndHorizontal ();
		GUILayout.EndVertical();
	}

    ///<summary>
    /// 對話內容設定列表
    /// </summary>
    ///<param name="key">資料索引值</param>
	public static void TextOutView (int key)
	{
		GUILayout.BeginVertical ();
			GUILayout.BeginHorizontal ();
				StoryEditorWindow._dialogue.storydata [key].parameter = ShowTextField ("名字：", StoryEditorWindow._dialogue.storydata [key].parameter, 30.0f, 200.0f);
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
				StoryEditorWindow._dialogue.storydata [key].parameter2 = ShowTextField ("對話：", StoryEditorWindow._dialogue.storydata [key].parameter2, 30.0f,350.0f);
			GUILayout.EndHorizontal ();
		GUILayout.EndVertical();
	}

    ///<summary>
    /// 延遲時間設定
    /// </summary>
    ///<param name="key">資料索引值</param>
	public static void DelayView (int key)
	{
		EditorGUIUtility.labelWidth = 30.0f;
		StoryEditorWindow._dialogue.storydata[key].parameter = ShowTextField("秒數：", StoryEditorWindow._dialogue.storydata[key].parameter, 30.0f, 200.0f);
	}

    ///<summary>
    /// 按鈕限制設定列表
    /// </summary>
    ///<param name="key">資料索引值</param>
	public static void ButtonFXView (int key)
	{
		GUILayout.BeginVertical ();
			GUILayout.BeginHorizontal ();
				ButtonFX._key [key] = ShowPopup("指令：", ButtonFX._key [key], ButtonFX._index, 30.0f, 120.0f);
				StoryEditorWindow._dialogue.storydata[key].parameter = ButtonFX._buttonFX[ButtonFX._key[key]];
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
				StoryEditorWindow._dialogue.storydata [key].parameter2 = ShowTextField(ButtonFX._paramIndex [ButtonFX._key [key]],
					StoryEditorWindow._dialogue.storydata [key].parameter2, 
					ButtonFX._labelWidth [ButtonFX._key [key]],
					100.0f);
			GUILayout.EndHorizontal ();
		GUILayout.EndVertical();
	}

    ///<summary>
    /// 玩家控制權限設定列表
    /// </summary>
    ///<param name="key">資料索引值</param>
	public static void ControlFXView (int key)
	{
		GUILayout.BeginVertical ();
			GUILayout.BeginHorizontal ();
				StoryEditorWindow._dialogue.storydata [key].parameter = ShowTextField ("開或關",
					StoryEditorWindow._dialogue.storydata [key].parameter,
					40.0f,
					200.0f);
			GUILayout.EndHorizontal ();
		GUILayout.EndVertical();
	}

    ///<summary>
    /// 故事結束撥放設定列表
    /// </summary>
    ///<param name="key">資料索引值</param>
	public static void EndView (int key)
	{
		GUILayout.BeginVertical ();
			GUILayout.BeginHorizontal ();
				StoryEditorWindow._dialogue.storydata [key].parameter = ShowTextField ("下一個場景名稱",
					StoryEditorWindow._dialogue.storydata [key].parameter,
					80.0f,
					200.0f);
			GUILayout.EndHorizontal ();
		GUILayout.EndVertical();
	}
}
