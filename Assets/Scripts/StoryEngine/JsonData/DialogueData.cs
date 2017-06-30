/*
 * 故事JSON格式
 * 編輯者:陳穎駿
 * 最後編輯日期:2017/06/01
*/
using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class Dialogue
{
    ///<summary>故事劇情</summary>
    public List<StoryData> storydata;
}

[Serializable]
public class StoryData
{
    ///<summary>UID</summary>
    public int id;
    ///<summary>功能</summary>
    public string command;
    ///<summary>參數1</summary>
    public string parameter;
    ///<summary>參數2</summary>
    public string parameter2;
}
