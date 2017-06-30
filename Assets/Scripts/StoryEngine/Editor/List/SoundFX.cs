/*
 * 音源設定列表腳本
 * 編輯者:陳穎駿
 * 最後編輯日期:2017/06/13
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundFX
{
	////<summary>下拉式選單Label寬度</summary>
	public static float _popLabelW = 30.0f;
	////<summary>下拉式選單寬度</summary>
	public static float _popWidth = 100.0f;
	////<summary>輸入框寬度</summary>
	public static float _width = 350.0f;
	////<summary>列表最後一項的Index</summary>
	public static int _lastIndex = 4;

    ///<summary>下拉式選單列表</summary>
	public static List<int> _key = new List<int>();

    ///<summary>讀取用英文指令</summary>
	public static string[] _soundFX = new string[]
	{
		"play_bgm",
		"play_se",
		"bgm_fade_in",
		"bgm_fade_out",
		"stop_bgm"
	};
    ///<summary>顯示用中文指令</summary>
    public static string[] _index = new string[]
    {
        "撥放音樂",
        "播放音效",
        "聲音淡入",
        "聲音淡出",
        "停止播放"
    };
    ///<summary>顯示用中文指令</summary>
    public static string[] _paramIndex = new string[]
    {
        "音樂 ID",
        "音效 ID",
        "秒數",
        "秒數",
    };
	///<summary>Label寬度</summary>
	public static int[] _labelW = new int[] 
	{
		50,
		50,
		30,
		30
	};
}
