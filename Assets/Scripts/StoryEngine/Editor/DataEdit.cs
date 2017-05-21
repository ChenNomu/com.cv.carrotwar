/*
 * 故事項目設定腳本
 * 編輯者:陳穎駿
 * 最後編輯日期:2017/05/21
*/
using UnityEngine;
using UnityEditor;
using System.Collections;

public class DataEdit : EditorWindow
{
    ///<summary>項目文字</summary>
    private static string _label;

    ///<summary>
    /// 功能設定列表
    /// </summary>
    ///<param name="key">資料索引值</param>
	public static void CommandView (int key)
	{
		EditorGUIUtility.labelWidth = 30.0f;
		Command.commandKey[key] = EditorGUILayout.Popup("功能", 
			Command.commandKey[key], 
			Command._commandIndex, 
			GUILayout.Width(120.0f));
		StoryEditorWindow._dialogue.storydata[key].command = Command._command[Command.commandKey[key]];
	}

    ///<summary>
    /// 讀取設定列表
    /// </summary>
    ///<param name="key">資料索引值</param>
    public static void LoadFXView (int key)
	{
		GUILayout.BeginVertical ();
			GUILayout.BeginHorizontal ();
				EditorGUIUtility.labelWidth = 30.0f;   
				LoadFX.commandLoadKey [key] = EditorGUILayout.Popup ("指令",
                    LoadFX.commandLoadKey [key], 
                    LoadFX._loadFXIndex, 
                    GUILayout.Width (100.0f));
				StoryEditorWindow._dialogue.storydata [key].parameter = LoadFX._loadFX [LoadFX.commandLoadKey [key]];
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
                _label = LoadFX._parameterIndex[LoadFX.commandLoadKey [key]];
				EditorGUIUtility.labelWidth = 50.0f;
                StoryEditorWindow._dialogue.storydata [key].parameter2 = EditorGUILayout.TextField (_label,
                    StoryEditorWindow._dialogue.storydata [key].parameter2, 
                    GUILayout.Width (350.0f));
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
                EditorGUIUtility.labelWidth = 30.0f;
                ScreenFX.commandScreenKey [key] = EditorGUILayout.Popup ("指令", 
                    ScreenFX.commandScreenKey [key], 
                    ScreenFX._screenFXIndex, 
                    GUILayout.Width (100.0f));
                StoryEditorWindow._dialogue.storydata [key].parameter = ScreenFX._screenFX [ScreenFX.commandScreenKey [key]];
            GUILayout.EndHorizontal ();

            GUILayout.BeginHorizontal ();
                _label = ScreenFX._parameterIndex[ScreenFX.commandScreenKey[key]];
                EditorGUIUtility.labelWidth = ScreenFX._labelWidth [ScreenFX.commandScreenKey [key]];
                StoryEditorWindow._dialogue.storydata [key].parameter2 = EditorGUILayout.TextField (_label, StoryEditorWindow._dialogue.storydata [key].parameter2, GUILayout.Width (350.0f));
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
                EditorGUIUtility.labelWidth = 30.0f;
                SoundFX.commandSoundFX [key] = EditorGUILayout.Popup ("指令：", 
                    SoundFX.commandSoundFX [key], 
                    SoundFX._soundFXIndex, 
                    GUILayout.Width (120.0f));
                StoryEditorWindow._dialogue.storydata[key].parameter = SoundFX._soundFX[SoundFX.commandSoundFX[key]];

            GUILayout.EndHorizontal ();

            GUILayout.BeginHorizontal ();
                if (SoundFX.commandSoundFX[key] != 4)
                {
                    _label = SoundFX._parameterIndex[SoundFX.commandSoundFX[key]];
                    EditorGUIUtility.labelWidth = 60.0f;
                    StoryEditorWindow._dialogue.storydata [key].parameter2 = EditorGUILayout.TextField (_label, StoryEditorWindow._dialogue.storydata [key].parameter2, GUILayout.Width (350.0f));
                }
            GUILayout.EndHorizontal ();
        GUILayout.EndVertical();


        // TODO.

    }
}
