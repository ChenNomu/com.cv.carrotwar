/*
 * 角色設定列表腳本
 * 編輯者:陳穎駿
 * 最後編輯日期:2017/06/13
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharFX
{
	////<summary>下拉式選單Label寬度</summary>
	public static float _popLabelW = 30.0f;
	////<summary>下拉式選單寬度</summary>
	public static float _popWidth = 130.0f;
	////<summary>輸入框寬度</summary>
	public static float _width = 350.0f;

	///<summary>下拉式選單列表</summary>
	public static List<int> _key = new List<int> ();

	///<summary>讀取用英文指令</summary>
	public static string[] _charFX = new string[]
	{
		"chara_l",
		"chara_r",
		"motion_l",
		"motion_r",
		"highlight_l",
		"highlight_r"
	};
	///<summary>顯示用中文指令</summary>
	public static string[] _index = new string[] 
	{
		"設定左方角色",
		"設定右方角色",
		"設定左方角色動作",
		"設定右方角色動作",
		"設定左方角色透明度",
		"設定右方角色透明度"
	};
	///<summary>顯示用中文指令</summary>
	public static string[] _paramIndex = new string[]
	{
		"角色名稱",
		"角色名稱",
		"動作名稱",
		"動作名稱",
		"透明度(0~1.0)",
		"透明度(0~1.0)",
		"透明度(0~1.0)"
	};
	///<summary>Label寬度</summary>
	public static int[] _labelW = new int[]
	{
		50,
		50,
		50,
		50,
		80,
		80,
		80
	};
}
