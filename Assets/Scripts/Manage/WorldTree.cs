using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 世界树全局参数设置
/// </summary>
public class WorldTreeSetting{
    /// <summary>
    /// 全局重力常量
    /// </summary>
    public  float gravity = 10;

    
}


/// <summary>
/// 世界树
/// </summary>
[System.Serializable]
public class WorldTree {
    /// <summary>
    /// 角色管理
    /// </summary>
    public static RoleManagement roleManagement;
    /// <summary>
    /// 剧情脚本控制器
    /// </summary>
    public static PlotEditor plotEditor;
    /// <summary>
    /// 触发器管理
    /// </summary>
    public static SceneTriggerManage triggerManage;
    /// <summary>
    /// Buff图表
    /// </summary>
    public static BuffMap buffMap;
    /// <summary>
    /// 世界UI模块
    /// </summary>
    public static WorldUI worldUI;
    /// <summary>
    /// 动态焦点摄像机系统
    /// </summary>
    public static DynamicFocusSystem dynamicFocusSystem;
    /// <summary>
    /// 本机的keyBuff
    /// </summary>
    public static KeyBuff keyBuff;
    public static WorldTreeSetting parameters;
    //public static 
    /// <summary>
    /// 静态构造器
    /// </summary>
    static WorldTree() {
        parameters = new WorldTreeSetting();
        roleManagement = new RoleManagement();
        triggerManage = new SceneTriggerManage();
        buffMap = new BuffMap();
        worldUI = new WorldUI();
    }
}
