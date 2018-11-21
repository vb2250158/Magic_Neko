using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 剧情脚本委托
/// </summary>
[System.Serializable]
public class ParseCode
{
    public string name;
    /// <summary>
    /// 触发的事件链
    /// </summary>
    public EventContent Content;

    [System.Serializable]
    public class EventContent : UnityEvent
    {

    }

}


public class ParseCodeLibrary : MonoBehaviour {
    /// <summary>
    /// 编译代码设置
    /// </summary>
    public List<ParseCode> parseCodes;

    /// <summary>
    /// 解析一个事件链
    /// </summary>
    /// <param name="eventName"></param>
    public void Parse(string contentValue ) {
       
        parseCodes.Find(delegate(ParseCode parseCode) {
            return parseCode.name == contentValue;
        }).
        Content.
        Invoke();
    }

  
}
