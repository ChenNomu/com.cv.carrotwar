﻿/*
 * 讀取功能列表腳本
 * 編輯者:陳穎駿
 * 最後編輯日期:2017/06/13
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadFX
{
	////<summary>下拉式選單Label寬度</summary>
	public static float _popLabelW = 30.0f;
	////<summary>下拉式選單寬度</summary>
	public static float _popWidth = 100.0f;
	////<summary>輸入框標籤寬度</summary>
	public static float _labelW = 50.0f;
	////<summary>輸入框寬度</summary>
	public static float _width = 350.0f;

    ///<summary>下拉式選單列表</summary>
	public static List<int> _key = new List<int>();

    ///<summary>讀取用英文指令</summary>
	public static string[] _loadFX = new string[]
	{
		"load_map",
		"load_story",
		"load_scene"
	};
    ///<summary>顯示用中文指令</summary>
	public static string[] _index = new string[]
	{
		"讀取地圖",
		"讀取故事",
		"讀取場景"
	};
    ///<summary>顯示用中文指令</summary>
    public static string[] _paramIndex = new string[]
    {
        "地圖檔名",
        "故事檔名",
        "場景檔名"
    };
}
