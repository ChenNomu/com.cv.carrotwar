/*
 * 故事引擎
 * 概要:
 * 讀取故事Json檔案並依照順序執行各個顯示面的功能。
 * 編輯者:陳穎駿
 * 最後編輯日期:2017/06/13
*/
using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace CommonManager
{
	public class StoryEngine : EngineBase
	{
        ///<summary>主要功能對照列表</summary>
        public Dictionary<string, Action> _cmdDic = new Dictionary<string, Action> ();
        ///<summary>次要功能數值對照表</summary>
        public Dictionary<string, Action> _paramDic = new Dictionary<string, Action> ();

        ///<summary>故事Json格式</summary>
        private Dialogue _dialogue = new Dialogue ();

        ///<summary>遮罩座標</summary>
        private Vector2 maskPosition = new Vector2();
        ///<summary>遮罩寬度與高度</summary>
        private int mask_W;
        private int mask_H;
        ///<summary>進行中的UID</summary>
        private int loadingUID = 0;
        ///<summary>故事間的延遲時間</summary>
		private float _delay = 0.5f;

        ///<summary>故事檔案名稱</summary>
        public string _file;

        ///<summary>切換下一段內容用的開關</summary>
		private bool m_next = true;

		void Start ()
		{
			EngineInit ();

			LoadFile ();

			EventDelegate.Add (_textButton.onClick, GoNext);

            SetProcess (_dialogue.storydata [loadingUID]);
		}

		void Update () 
		{
			if (Input.GetKeyDown (KeyCode.F2)) 
			{
				m_next = true;
			}

			if (Input.GetKeyDown (KeyCode.F3)) 
			{
				GoNext ();
			}

            StartStory ();
		}

        ///<summary>
        /// 進行故事
        /// </summary>
        private void StartStory ()
        {
            if (m_next) 
            {
                m_next = false;
                SetProcess (_dialogue.storydata [loadingUID]);
                Process ();
            }
        }

        ///<summary>
        /// 設定下一個要處理的故事項目
        /// </summary>
        ///<param name="data">要讀取的資料</param>
		private void SetProcess(StoryData data)
		{
			StoryProcess._command = data.command;
			StoryProcess._param = data.parameter;
			StoryProcess._param2 = data.parameter2;
		}

        ///<summary>
        /// 進行播放故事
        /// </summary>
		private void Process ()
		{
			if (_cmdDic.ContainsKey (StoryProcess._command)) 
			{
				_cmdDic [StoryProcess._command] ();
			}
		}

        ///<summary>
        /// 根據主要項目的類別進行演出
        /// </summary>
		private void FX_Action ()
		{
			if (_paramDic.ContainsKey (StoryProcess._param))
			{
				_paramDic [StoryProcess._param] ();
			}
		}

        ///<summary>
        /// 讀取故事檔案
        /// </summary>
		private void LoadFile ()
		{
			TextAsset _text = Resources.Load<TextAsset> ("Dialogue/" + _file);

			try
			{
				_dialogue = JsonUtility.FromJson<Dialogue> (_text.text);
			}
			catch(Exception ex)
			{
				Debug.LogError ("(" + ex.Message + ")" + " : File Name " + _file);
                // TODO. 顯示錯誤訊息並且回到標題畫面
				return;
			}
		}

        ///<summary>
        /// 顯示對話內容
        /// </summary>
		private void TextOut ()
		{
			_textBG.SetActive (true);
			_textName.text = StoryProcess._param;
			_textOut.text = StoryProcess._param2;
			StartCoroutine (DelayToNext());
		}

        ///<summary>
        /// 進行新手教學
        /// </summary>
		private void Tutorial ()
		{
			_mask.SetActive (true);

            SetMaskTargetPos();
            SetMaskTargetSize();
		}

        ///<summary>
        /// 自動下一段前進行延遲動作
        /// </summary>
		private IEnumerator DelayToNext ()
		{
			yield return new WaitForSeconds (_delay);

			_textNext.SetActive (true);
		}

        ///<summary>
        /// 設定延遲時間並進行延遲動作
        /// </summary>
		private void Delay ()
		{
			_delay = float.Parse (StoryProcess._param);
            StartCoroutine (DelayToNext());
		}

        ///<summary>
        /// 讀取故事內容
        /// </summary>
		private void Load_Story()
		{
			_file = StoryProcess._param2;
			LoadFile ();
            loadingUID = StorySetting.firstIndex;
			m_next = true;
		}

        ///<summary>
        /// 讀取故事以外的東西(場景或關卡)
        /// </summary>
		private void Load_Other ()
		{
			gameManager.LoadScene (StoryProcess._param2);
            loadingUID++;
		}

        ///<summary>
        /// 設定背景圖案
        /// </summary>
		private void Set_Background ()
		{
			_background.mainTexture = LoadTexture ("Background");
			GoNext ();
		}

        ///<summary>
        /// 故事大標題(章)文字
        /// </summary>
		private void Set_StoryTitle ()
		{
			_mTitle.gameObject.SetActive (true);
			_mTitle.text = StoryProcess._param2;
		}

        ///<summary>
        /// 故事小標題(節或故事地點)文字
        /// </summary>
		private void Set_Title ()
		{
			_sTitleBG.SetActive (true);
			_sTitle.text = StoryProcess._param2;
			GoNext ();
		}

        ///<summary>
        /// 設定角色
        /// </summary>
		private void Set_Chara ()
		{
            UITexture _texture = GetCharaTexture ();

			_texture.mainTexture = LoadTexture ("Chara");
			GoNext ();
		}

        ///<summary>
        /// 設定角色明暗度
        /// </summary>
		private void Set_HighLight ()
		{
            UITexture _texture = GetCharaTexture ();

			_texture.color = SetCharaColor ();
			GoNext ();
		}

        ///<summary>
        /// 取得角色貼圖
        /// </summary>
		private UITexture GetCharaTexture ()
		{
            string chara_rot = StoryProcess._param.Substring (StoryProcess._param.Length - StorySetting.indexCount);

			return (chara_rot == "l") ? _chara_l : _chara_r;
		}

        ///<summary>
        /// 讀取圖檔
        /// </summary>
		private Texture LoadTexture (string folder)
		{
			return Resources.Load (folder + "/" + StoryProcess._param2) as Texture;
		}

        ///<summary>
        /// 設定角色顏色
        /// </summary>
		private Color SetCharaColor ()
		{
			float _value = float.Parse (StoryProcess._param2);
			return new Color(_value, _value, _value, 1);
		}

        ///<summary>
        /// 設定動態透明度
        /// </summary>
		private void SetTweenAlpha ()
		{
			_effectTween.duration = float.Parse (StoryProcess._param2);
			_effectTween.enabled = true;

			if (StoryProcess._param == "pic_fade_out") 
			{
				_effectTween.PlayReverse ();
			}
		}

        ///<summary>
        /// 執行下一段故事內容
        /// </summary>
		public void GoNext ()
		{
            loadingUID++;
			m_next = true;

            // @如果遮罩是打開的狀態時，先暫時將遮罩關閉防止遮罩殘留在畫面上
			if (_mask.activeSelf) 
			{
				_mask.SetActive (false);
			}
		}

        ///<summary>
        /// 引擎初始化
        /// </summary>
		private void EngineInit ()
		{
			gameManager = GameObject.Find ("GameManager").GetComponent<Game_Manager> ();

			_cmdDic.Add ("load_fx", FX_Action);
			_cmdDic.Add ("screen_fx", FX_Action);
			_cmdDic.Add ("sound_fx", FX_Action);
			_cmdDic.Add ("char_fx", FX_Action);
			_cmdDic.Add ("text_out", TextOut);
			_cmdDic.Add ("delay", Delay);
			_cmdDic.Add ("tutorial", Tutorial);
            // TODO. 設定故事模式結束後的動作
			//_cmdDic.Add ("end", );

			_paramDic.Add ("load_story", Load_Story);
			_paramDic.Add ("load_map", Load_Other);
			_paramDic.Add ("load_scene", Load_Other);
			_paramDic.Add ("set_background", Set_Background);
			_paramDic.Add ("set_storytitle", Set_StoryTitle);
			_paramDic.Add ("set_title", Set_Title);
			_paramDic.Add ("pic_fade_in", SetTweenAlpha);
			_paramDic.Add ("pic_fade_out", SetTweenAlpha);
            // TODO. 設定故事模式介面開關
			//_paramDic.Add ("ui_show");
            // TODO. 螢幕搖晃功能
			//_paramDic.Add ("shake");
            // TODO. 播放背景音樂
			//_paramDic.Add ("play_bgm");
            // TODO. 播放音效
			//_paramDic.Add ("play_se");
            // TODO. 背景音樂淡入
			//_paramDic.Add ("bgm_fade_in");
            // TODO. 背景音樂淡出
			//_paramDic.Add ("bgm_fade_out");
            // TODO. 停止音樂
			//_paramDic.Add ("stop_bgm");
			_paramDic.Add ("chara_l", Set_Chara);
			_paramDic.Add ("chara_r", Set_Chara);
            // TODO. 設定左邊角色動作
			//_paramDic.Add ("motion_l");
            // TODO. 設定右邊角色動作
			//_paramDic.Add ("motion_r");
			_paramDic.Add ("highlight_l", Set_HighLight);
			_paramDic.Add ("highlight_r", Set_HighLight);
		}

        ///<summary>
        /// 設定遮罩座標
        /// </summary>
        private void SetMaskTargetPos ()
        {
            string[] _vec = StoryProcess._param.Split (',');

            maskPosition = new Vector2 (float.Parse(_vec[0]), float.Parse(_vec[1]));

            _targetBtn.transform.localPosition = maskPosition;
        }

        ///<summary>
        /// 設定遮罩大小
        /// </summary>
        private void SetMaskTargetSize()
        {
            string[] _size = StoryProcess._param2.Split (',');

            mask_W = int.Parse(_size[0]);
            mask_H = int.Parse(_size[1]);

            _targetBtn.SetDimensions (mask_W, mask_H);
        }
	}
}
