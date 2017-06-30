/*
 * 故事引擎設定檔腳本
 * 編輯者:陳穎駿
 * 最後編輯日期:2017/06/21
*/
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace CommonManager
{
	public class EngineBase : MonoBehaviour
	{
		///<summary>遊戲管理者</summary>
		public Game_Manager gameManager;

		///<summary>遊戲大標題(章)Label</summary>
		public UILabel _mTitle;
		///<summary>遊戲小標題(節、地點名稱等)Label</summary>
		public UILabel _sTitle;
		///<summary>對話角色名字物件Label</summary>
		public UILabel _textName;
		///<summary>對話內容物件Label</summary>
		public UILabel _textOut;

		///<summary>背景圖案</summary>
		public UITexture _background;
		///<summary>淡入淡出用</summary>
		public UITexture _effect;
		///<summary>左邊角色</summary>
		public UITexture _chara_l;
		///<summary>右邊角色</summary>
		public UITexture _chara_r;
		///<summary>新手教學時的目標點</summary>
		public UITexture _targetBtn;

		///<summary>淡入淡出用動態設定</summary>
		public TweenAlpha _effectTween;

		///<summary>小標題背景圖</summary>
		public GameObject _sTitleBG;
		///<summary>對話框背景圖</summary>
		public GameObject _textBG;
		///<summary>對話框</summary>
		public GameObject _textNext;
		///<summary>新手教學遮罩</summary>
		public GameObject _mask;

		///<summary>對話框按鈕</summary>
		public UIButton _textButton;
	}

	public class StoryProcess
	{
		///<summary>故事中的功能</summary>
		public static string _command;
		///<summary>故事功能中的參數1</summary>
		public static string _param;
		///<summary>故事功能中的參數2</summary>
		public static string _param2;
	}

    public class StorySetting
    {
		///<summary>故事json檔資料筆數</summary>
        public static int indexCount = 1;
		///<summary>故事json檔初始的資料ID</summary>
        public static int firstIndex = 0;
    }
}
