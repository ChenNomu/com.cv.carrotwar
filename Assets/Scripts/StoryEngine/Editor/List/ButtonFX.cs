using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonFX
{
	public static List<int> _key = new List<int>();


	public static string[] _buttonFX = new string[]
	{
		"set_active",
		"set_active_all"
	};

	public static string[] _index = new string[]
	{
		"設定單個按鈕開關",
		"設定所有按鈕開關"
	};

	public static string[] _paramIndex = new string[]
	{
		"按鈕名稱,開或關",
		"開或關"
	};

	public static int[] _labelWidth = new int[] 
	{
		150,
		40
	};
}
