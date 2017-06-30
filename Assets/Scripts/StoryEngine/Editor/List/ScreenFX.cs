/*
 * 畫面設定列表腳本
 * 編輯者:陳穎駿
 * 最後編輯日期:2017/06/13
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScreenFX
{
	////<summary>下拉式選單Label寬度</summary>
	public static float _popLabelW = 30.0f;
	////<summary>下拉式選單寬度</summary>
	public static float _popWidth = 100.0f;
	////<summary>輸入框寬度</summary>
	public static float _width = 350.0f;

    ///<summary>下拉式選單列表</summary>
	public static List<int> _key = new List<int>();

    ///<summary>讀取用英文指令</summary>
	public static string[] _screenFX = new string[] 
	{
		"set_background",
		"set_storytitle",
		"set_title",
		"pic_fade_in",
		"pic_fade_out",
		"ui_show",
		"shake"
	};
    ///<summary>顯示用中文指令</summary>
	public static string[] _index = new string[] 
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
	public static string[] _paramIndex = new string[] 
	{
		"檔名",
		"標題",
		"標題",
		"秒數",
		"秒數",
		"開關",
		"持續時間,幅度,速度"
	};
    ///<summary>Label寬度</summary>
	public static int[] _labelW = new int[] 
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
