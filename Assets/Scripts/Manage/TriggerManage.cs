using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 触发器管理
/// </summary>
[System.Serializable]
public class SceneTriggerManage  {

    private List<SceneTriggerEditor> triggers = new List<SceneTriggerEditor>();

    /// <summary>
    /// 添加游戏触发器
    /// </summary>
    /// <param name="gameTrigger"></param>
    /// <returns></returns>
    public bool AddTrigger(SceneTriggerEditor gameTrigger) {
        triggers.Add(gameTrigger);
        return true;
    }
}
