using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Buff类
/// </summary>
[System.Serializable]
public class Buff
{
    /// <summary>
    /// buff名
    /// </summary>
    public string name;
    /// <summary>
    /// buff计时
    /// </summary>
    public float Timer;
    /// <summary>
    /// buff说明
    /// </summary>
    public string Explain;
    /// <summary>
    /// 角色的update时调用
    /// </summary>
    public event BuffUpdate OnUpdate;
    /// <summary>
    /// 当获得buff时调用
    /// </summary>
    public event BuffUpdate OnAdd;
    /// <summary>
    /// 当buff被移除时调用
    /// </summary>
    public event BuffUpdate OnRemove;
    /// <summary>
    /// 角色更新
    /// </summary>
    public void RoleUpdate(Role role)
    {
        OnUpdate(role);
    }
    /// <summary>
    /// 触发角色添加buff
    /// </summary>
    /// <param name="role"></param>
    public void RoleAddBuff(Role role)
    {
        OnAdd(role);
    }
    /// <summary>
    /// 角色移除buff
    /// </summary>
    /// <param name="role"></param>
    public void RoleRemoveBuff(Role role)
    {
        OnRemove(role);
    }

    
}


[System.Serializable]
public delegate void BuffUpdate(Role role);