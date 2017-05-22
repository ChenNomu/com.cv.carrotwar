using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharFX
{
	public static List<int> _key = new List<int> ();
	public static string[] _charFX = new string[]
	{
		"set_chara_l",
		"set_chara_r",
		"set_chara_c",
		"set_motion_l",
		"set_motion_r",
		"set_motion_c",
		"highlight_l",
		"highlight_r",
		"highlight_c"
	};

	public static string[] _index = new string[] 
	{
		"設定左方角色",
		"設定右方角色",
		"設定中間角色",
		"設定左方角色動作",
		"設定右方角色動作",
		"設定中間角色動作",
		"設定左方角色透明度",
		"設定右方角色透明度",
		"設定中間角色透明度"
	};

	public static string[] _paramIndex = new string[]
	{
		"角色名稱",
		"角色名稱",
		"角色名稱",
		"動作名稱",
		"動作名稱",
		"動作名稱",
		"透明度",
		"透明度",
		"透明度"
	};
}
