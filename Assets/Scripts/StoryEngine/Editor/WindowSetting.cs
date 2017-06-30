/*
 * 故事編輯器設定檔
 * 編輯者:陳穎駿
 * 最後編輯日期:2017/06/13
*/
using UnityEngine;
using UnityEditor;
using System.Collections;

public class WindowSetting
{
	///<summary>視窗大小</summary>
	public static Vector2 _windowSize = new Vector2(800.0f, 600.0f); 
}

public class UISetting
{
	///<summary>檔案路徑Label寬度</summary>
	public static float _pathLabelW = 60.0f;
	///<summary>檔案路徑顯示寬度</summary>
	public static float _textFieldW = 700.0f;
	///<summary>新增檔案按鈕寬度</summary>
	public static float _createStorybtnW = 120.0f;
	///<summary>讀取按鈕寬度</summary>
	public static float _loadStoryBtnW = 90.0f;
	///<summary>刪除按鈕寬度</summary>
	public static float _deleteDataBtnW = 50.0f;
	///<summary>滾軸長度寬度</summary>
	public static float _scrollH = 530.0f;
	public static float _scrollW = 800.0f;

	///<summary>共用Label內容</summary>
    public static string _baseLabel = "指令";
	///<summary>Command Label內容</summary>
    public static string _cmdLabel = "功能";
	///<summary>對話框姓名欄位Label內容</summary>
	public static string _textOutNameLabel = "名字：";
	///<summary>對話框Label內容</summary>
	public static string _textOutLabel = "對話：";
	///<summary>延遲時間Label內容</summary>
	public static string _delayLabel = "秒數：";
	///<summary>新手教學座標Label內容</summary>
	public static string _tutPosLabel = "座標x,y,x：";
	///<summary>新手教學顯示大小Label內容</summary>
	public static string _tutSizeLabel = "範圍大小：";
	///<summary>結束Label內容</summary>
	public static string _endLabel = "下一個場景名稱";
}

public class DataSetting
{
	///<summary>資料數為0的時候用的</summary>
	public static int _null = -1;
	///<summary>資料為0時用的</summary>
	public static int _zero = 0;
	///<summary>資料為最後一筆的時候用的</summary>
	public static int _lastData = 1;
}
