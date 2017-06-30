/*
 * 故事項目設定腳本
 * 編輯者:陳穎駿
 * 最後編輯日期:2017/06/11
*/
using UnityEngine;
using UnityEditor;
using System.Collections;

public class DataEdit : EditorWindow
{
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
        Command._key [key] = ShowPopup (UISetting._cmdLabel, 
            Command._key [key], 
            Command._index, 
			Command._popLabelW,
			Command._popWidth);
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
            	LoadFX._key [key] = ShowPopup(UISetting._baseLabel, 
            		LoadFX._key [key], 
            		LoadFX._index, 
					LoadFX._popLabelW, 
					LoadFX._popWidth);
				StoryEditorWindow._dialogue.storydata [key].parameter = LoadFX._loadFX [LoadFX._key [key]];
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
				StoryEditorWindow._dialogue.storydata [key].parameter2 = ShowTextField (LoadFX._paramIndex [LoadFX._key [key]],
					StoryEditorWindow._dialogue.storydata [key].parameter2, 
					LoadFX._labelW,
					LoadFX._width);
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
				ScreenFX._key [key] = ShowPopup (UISetting._baseLabel, 
					ScreenFX._key [key], 
					ScreenFX._index, 
					ScreenFX._popLabelW, 
					ScreenFX._popWidth);
				StoryEditorWindow._dialogue.storydata [key].parameter = ScreenFX._screenFX [ScreenFX._key [key]];
            GUILayout.EndHorizontal ();

            GUILayout.BeginHorizontal ();
				StoryEditorWindow._dialogue.storydata [key].parameter2 = ShowTextField (ScreenFX._paramIndex [ScreenFX._key [key]],
					StoryEditorWindow._dialogue.storydata [key].parameter2,
					ScreenFX._labelW [ScreenFX._key [key]],
					ScreenFX._width);
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
				SoundFX._key [key] = ShowPopup (UISetting._baseLabel, 
					SoundFX._key [key], 
					SoundFX._index, 
					SoundFX._popLabelW, 
					SoundFX._popWidth);
				StoryEditorWindow._dialogue.storydata[key].parameter = SoundFX._soundFX[SoundFX._key[key]];
            GUILayout.EndHorizontal ();

            GUILayout.BeginHorizontal ();
				if (SoundFX._key[key] != SoundFX._lastIndex)
                {
					StoryEditorWindow._dialogue.storydata [key].parameter2 = ShowTextField (SoundFX._paramIndex [SoundFX._key [key]],
						StoryEditorWindow._dialogue.storydata [key].parameter2,
						SoundFX._labelW [SoundFX._key [key]],
						SoundFX._width);
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
				CharFX._key [key] = ShowPopup (UISetting._baseLabel, 
					CharFX._key [key], 
					CharFX._index, 
					CharFX._popLabelW, 
					CharFX._popWidth);
				StoryEditorWindow._dialogue.storydata[key].parameter = CharFX._charFX[CharFX._key [key]];
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
				StoryEditorWindow._dialogue.storydata [key].parameter2 = ShowTextField (CharFX._paramIndex [CharFX._key [key]],
					StoryEditorWindow._dialogue.storydata [key].parameter2,
					CharFX._labelW [CharFX._key [key]],
					CharFX._width);
			GUILayout.EndHorizontal ();
		GUILayout.EndVertical();
	}

	///<summary>
	/// 對話設定列表
	/// </summary>
	///<param name="key">資料索引值</param>
	public static void TextOutView (int key)
	{
		GUILayout.BeginVertical ();
			GUILayout.BeginHorizontal ();
				StoryEditorWindow._dialogue.storydata [key].parameter = ShowTextField (UISetting._textOutNameLabel, 
					StoryEditorWindow._dialogue.storydata [key].parameter, 
					TextOut._labelW,
					TextOut._width);
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
				StoryEditorWindow._dialogue.storydata [key].parameter2 = ShowTextField (UISetting._textOutLabel, 
					StoryEditorWindow._dialogue.storydata [key].parameter2, 
					TextOut._labelW2,
					TextOut._width2);
			GUILayout.EndHorizontal ();
		GUILayout.EndVertical();
	}

	///<summary>
	/// 延遲設定
	/// </summary>
	///<param name="key">資料索引值</param>
	public static void DelayView (int key)
	{
		EditorGUIUtility.labelWidth = Delay._labelW;
			StoryEditorWindow._dialogue.storydata[key].parameter = ShowTextField(UISetting._delayLabel, 
			StoryEditorWindow._dialogue.storydata[key].parameter, 
			Delay._labelW,
			Delay._width);
	}

	///<summary>
	/// 按鈕開關設定列表
	/// </summary>
	///<param name="key">資料索引值</param>
	public static void TutorialView (int key)
	{
		GUILayout.BeginVertical ();
			GUILayout.BeginHorizontal ();
				StoryEditorWindow._dialogue.storydata [key].parameter = ShowTextField (UISetting._tutPosLabel, 
					StoryEditorWindow._dialogue.storydata [key].parameter, 
					Tutorial._labelW,
					Tutorial._width);
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
				StoryEditorWindow._dialogue.storydata [key].parameter2 = ShowTextField (UISetting._tutSizeLabel, 
					StoryEditorWindow._dialogue.storydata [key].parameter2, 
					Tutorial._labelW2,
					Tutorial._width2);
			GUILayout.EndHorizontal ();
		GUILayout.EndVertical();
	}

	///<summary>
	/// 結束設定
	/// </summary>
	///<param name="key">資料索引值</param>
	public static void EndView (int key)
	{
		GUILayout.BeginVertical ();
			GUILayout.BeginHorizontal ();
				StoryEditorWindow._dialogue.storydata [key].parameter = ShowTextField (UISetting._endLabel,
					StoryEditorWindow._dialogue.storydata [key].parameter,
					End._labelW,
					End._width);
			GUILayout.EndHorizontal ();
		GUILayout.EndVertical();
	}
}
