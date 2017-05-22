/*
 * 功能設定列表腳本
 * 編輯者:陳穎駿
 * 最後編輯日期:2017/05/21
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Command
{
    ///<summary>下拉式選單列表</summary>
	public static List<int> _key = new List<int>();
    ///<summary>讀取用英文指令</summary>
	public static string[] _command = new string[] 
	{
		"load_fx",
		"screen_fx",
		"sound_fx",
		"char_fx",
		"text_out",
		"delay",
		"button_fx",
		"control_fx",
		"end"
	};
    ///<summary>顯示用中文指令</summary>
    public static string[] _index = new string[]
    {
        "讀取資料",
        "畫面設定",
        "音源設定",
        "角色設定",
        "對話設定",
        "延遲時間",
        "設定按鈕功能",
        "設定玩家控制權",
        "結束"
    };
}
