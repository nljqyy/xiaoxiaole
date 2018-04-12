using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExcelScriptObj : ScriptableObject
{
    public List<ExcelTableEntity> voiceData;
    public List<ExcelTableEntity> voiceType;
}
 
[Serializable]
public class ExcelTableEntity
{
    public string ID;
    public string Time;
    public string TimeContent;
    public string WinningContent;
    public string FailContent;
    public string Type;
    public string Name;
    public string WinTime;
    public string FailTime;
    public string WinningAfter;
    public string WinningAfterTime;
}