/*
 * 畫面設定列表腳本
 * 編輯者:陳穎駿
 * 最後編輯日期:2017/05/21
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScreenFX
{
    ///<summary>下拉式選單列表</summary>
	public static List<int> commandScreenKey = new List<int>();
    ///<summary>讀取用英文指令</summary>
	public static string[] _screenFX = new string[] 
	{
		"ui_hide",
		"set_storytitle",
		"set_title",
		"fade_in",
		"fade_out",
		"ui_show",
		"shake"
	};
    ///<summary>顯示用中文指令</summary>
	public static string[] _screenFXIndex = new string[] 
	{
		"設定背景",
		"設定大標題",
		"設定小標題",
		"淡入",
		"淡出",
		"介面控制",
		"畫面震動"
	};
    ///<summary>顯示用中文指令</summary>
	public static string[] _parameterIndex = new string[] 
	{
		"檔名",
		"標題",
		"標題",
		"秒數",
		"秒數",
		"開關",
		"持續時間,幅度,速度"
	};
    ///<summary>Label長度</summary>
	public static int[] _labelWidth = new int[] 
	{
		30,
		30,
		30,
		30,
		30,
		30,
		110
	};
}
